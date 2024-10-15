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
    public int Streak = 0;
    public int HighestStreak = 0;
    public bool Finished = false;
    public int Misses = 0;
    public int AirStrikes = 0;
    public int CoinsAwarded => Mathf.Max(0, (int)(1000 * Percentage));

    public float Percentage => Score / (MaxScore <= 0 ? 1f : MaxScore);

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

    public int StreakLevel
    {
        get
        {
            if (Streak < 5) return 0;
            if (Streak <= 10) return 1;
            if (Streak <= 20) return 2;
            if (Streak <= 30) return 3;
            return 4;
        }
    }

    public float StreakMultiplier => 1.0f + (StreakLevel * 0.5f);

    public GameData EmptyCopy()
    {
        return new GameData()
        {
            SongName = this.SongName,
            DisplayTitle = this.DisplayTitle,
            DisplayArtist = this.DisplayArtist,
        };
    }
}
