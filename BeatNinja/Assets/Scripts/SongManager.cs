using System.Collections.Generic;
using UnityEngine;

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

            Debug.Log($"Found Song: {wavPath}");
            Songs.Add(new Song(WavUtility.ToAudioClip(wavPath), new MidiFile(file)));
        }
    }
}