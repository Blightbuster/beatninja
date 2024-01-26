using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public enum PlayRating
    {
        F,
        D,
        C,
        B,
        A,
        S,
        S_Plus

    }

    public int Score = 0;
    public int HighestStreak = 0;
    public PlayRating Rating = PlayRating.F;
    public bool Finished = false;
    public int Misses = 0;
    public int AirStrikes = 0;
}
