using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class Song
{
    public AudioClip Audio;
    public MidiFile Midi;
    public Queue<SongEvent> Events;
    public int BPM;

    /// <summary>
    /// Only songs with non changing BPM can be parsed
    /// </summary>
    public Song(AudioClip audio, MidiFile midi)
    {
        Audio = audio;
        Midi = midi;
        BPM = GetBPM();
        Events = ParseMidi();
    }

    private Queue<SongEvent> ParseMidi()
    {
        var events = new List<SongEvent>();
        foreach (var track in Midi.Tracks)
        {
            // Skip if track has no name to match on
            if (track.TextEvents.Count == 0) continue;

            var name = track.TextEvents.Where(e => e.Type == (byte)TextEventType.TrackName).Select(e => e.Value).First();

            var trackEvents = name switch
            {
                Config.MidiParsing.LeftSpawnTrackName => ParseSpawnTrack(track, SpawnerSide.Left),
                Config.MidiParsing.RightSpawnTrackName => ParseSpawnTrack(track, SpawnerSide.Right),
                _ => new(), // return empty list if the track name is unknown
            };
            events.AddRange(trackEvents);
        }

        // Sort events and return them as a queue for faster access
        events.Sort((a, b) => a.Time.CompareTo(b.Time));
        return new Queue<SongEvent>(events);
    }

    private int GetBPM()
    {
        return Midi.Tracks.Select(track => track.MidiEvents
            .Where(e => e.Type == (byte)MidiEventType.MetaEvent).DefaultIfEmpty()
            .Where(e => e.MetaEventType == MetaEventType.Tempo).DefaultIfEmpty()
            .Select(e => e.Arg2).Max()
        ).Max();
    }

    private List<SongEvent> ParseSpawnTrack(MidiTrack track, SpawnerSide side)
    {
        var events = new List<SongEvent>();
        var notesDown = new List<MidiEvent>();

        foreach (var e in track.MidiEvents)
        {
            if (e.Type == (byte)MidiEventType.NoteOn)
            {
                // Check if note is already held down and add it if not
                if (!notesDown.Any(n => n.Note == e.Note)) notesDown.Add(e);
            }
            else if (e.Type == (byte)MidiEventType.NoteOff)
            {
                var off = e;

                // This should never throw an exception since every noteOff event must be preceded by a noteOn event
                var on = notesDown.Where(n => n.Note == off.Note).First();
                notesDown.Remove(on);
                
                var duration = TicksToTime(off.Time - on.Time);

                SpawnEvent spawnEvent = (on.Note - GetOffset(side)) switch
                {
                    Config.MidiParsing.SpawnNote => new SpawnNoteEvent(),
                    Config.MidiParsing.SpawnDoubleNote => new SpawnNoteEvent() { HitsNeeded = 2 },
                    Config.MidiParsing.SpawnSpamNote => new SpawnSpamNoteEvent() { Duration = duration},
                    _ => throw new ArgumentOutOfRangeException($"Unknown spawn event {on.Note}"),
                };

                // Set correct spawning side and time
                spawnEvent.Side = side;
                spawnEvent.Time = TicksToTime(on.Time);

                events.Add(spawnEvent);
            }
        }
        return events;
    }

    private int GetOffset(SpawnerSide side) => side switch
    {
        SpawnerSide.Left => Config.MidiParsing.LeftSpawnOffset,
        SpawnerSide.Right => Config.MidiParsing.RightSpawnOffset,
    };

    private float TicksToTime(int ticks) => 60f / (float)(BPM * Midi.TicksPerQuarterNote) * (float)ticks;
}

public enum SpawnerSide
{
    Left,
    Right,
}

public abstract class SongEvent
{
    public float Time;
}

public abstract class SpawnEvent : SongEvent
{
    public SpawnerSide Side;
}


public class SpawnNoteEvent : SpawnEvent
{
    public int HitsNeeded = 1;
}

/// <summary>
/// Spam notes freeze in place, when they are first hit.
/// They can be hit infinitely often while they are in the hit area.
/// They only get sliced once their duration expires.
/// </summary>
public class SpawnSpamNoteEvent : SpawnEvent
{
    public float Duration;
}
