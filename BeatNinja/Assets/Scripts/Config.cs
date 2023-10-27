public sealed class Config
{
    public static bool AlwaysAllowSlicing = true;
    public static int MissPenalty = -10;
    public static int MaxHitPoints = 100;

    public static float SliceableFlightOffset = -2.5f;

    public sealed class MidiParsing
    {
        public const string SpawnTrackName = "Spawn";
        public const int LeftSpawnNote = 94;
        public const int RightSpawnNote = 95;
    }
}
