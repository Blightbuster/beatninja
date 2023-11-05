public sealed class Config
{
    public static bool AlwaysAllowSlicing = true;
    public static int MissPenalty = -10;
    public static int MaxHitPoints = 100;

    public static float SliceableFlightOffset = -2.5f;

    public sealed class MidiParsing
    {
        public const string LeftSpawnTrackName = "beatninja-spawn-left";
        public const int LeftSpawnOffset = 96;      // C7 Note

        public const string RightSpawnTrackName = "beatninja-spawn-right";
        public const int RightSpawnOffset = 101;    // F7 Note

        public const int SpawnNote = 0;
        public const int SpawnDoubleNote = 1;
        public const int SpawnSpamNote = 2;

    }

    public sealed class User {
        public static float LatencyOffset = 0f;
    }
}
