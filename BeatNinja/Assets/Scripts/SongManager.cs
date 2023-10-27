using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class SongManager : MonoBehaviour
{
    public List<Song> Songs = new();

    private void Awake()
    {
        var files = System.IO.Directory.EnumerateFiles(Application.streamingAssetsPath + "/Songs", "*.mid");
        foreach (var file in files)
        {
            var wavPath = file.Replace(".mid", ".wav");
            if (!System.IO.File.Exists(wavPath))
            {
                Debug.LogError($"Unable to find {wavPath}");
                continue;
            }

            Songs.Add(new Song(WavUtility.ToAudioClip(wavPath), new MidiFile(file)));
        }
    }
}

public class Song
{
    public AudioClip Audio;
    public MidiFile Midi;
    public Queue<SongEvent> Events = new();
    public int BPM;

    // Only songs with non changing BPM can be parsed
    public Song(AudioClip audio, MidiFile midi)
    {
        Audio = audio;
        Midi = midi;
        ParseMidi();
    }

    private void ParseMidi()
    {
        foreach (var track in Midi.Tracks)
        {
            var name = track.TextEvents.Where(e => e.Type == (byte)TextEventType.TrackName).Select(e => e.Value).First();
            var bpm = track.MidiEvents.Where(e => e.Type == (byte)MidiEventType.MetaEvent)
                .Where(e => e.MetaEventType == MetaEventType.Tempo).Select(e => e.Arg2).FirstOrDefault();
            if (bpm != 0) this.BPM = bpm;
            if (name == Config.MidiParsing.SpawnTrackName) ParseSpawnTrack(track);

        }
    }

    private void ParseSpawnTrack(MidiTrack track)
    {
        foreach (var e in track.MidiEvents)
        {
            if (e.Type == (byte)MidiEventType.NoteOn)
            {
                var isLeft = e.Note == Config.MidiParsing.LeftSpawnNote;
                var isRight = e.Note == Config.MidiParsing.RightSpawnNote;
                if (isLeft == false && isRight == false)
                {
                    Debug.LogWarning($"Unknown spawn event {e.Note}");
                    continue;
                }

                if (isLeft) Events.Enqueue(new LeftSpawnEvent() { Time = TicksToTime(e.Time) });
                if (isRight) Events.Enqueue(new RightSpawnEvent() { Time = TicksToTime(e.Time) });
            }
        }
    }

    private float TicksToTime(int ticks) => 60f / (float)(BPM * Midi.TicksPerQuarterNote) * (float)ticks;
}

public abstract class SongEvent
{
    public float Time;
}

public class LeftSpawnEvent : SongEvent { }
public class RightSpawnEvent : SongEvent { }
