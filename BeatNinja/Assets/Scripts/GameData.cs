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
    public int CoinsAwarded => Score *1000 / (MaxScore <= 0 ? 1 : MaxScore);

    public int Rating
    {
        get
        {
            float ratio = Score / (MaxScore <= 0 ? 1f : (float)MaxScore);
            if (ratio < 0.5f) return 0;
            if (ratio <= 0.75f) return 1;
            if (ratio <= 0.9f) return 2;
            return 3;
        }
    }
}
