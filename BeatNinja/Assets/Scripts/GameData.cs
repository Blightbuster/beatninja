using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public string DisplayTitle;
    public string DisplayArtist;
    public string SongName;
    public int MaxScore = 0;
    public int Score = 0;
    public int HighestStreak = 0;
    public bool Finished = false;
    public int Misses = 0;
    public int AirStrikes = 0;
    public int CoinsAwarded => (int)(1000 * Percentage);

    public float Percentage => Score / (MaxScore <= 0 ? 1f : (float)MaxScore);

    public int Rating
    {
        get
        {
            if (Percentage < 0.5f) return 0;
            if (Percentage <= 0.75f) return 1;
            if (Percentage <= 0.9f) return 2;
            return 3;
        }
    }
}
