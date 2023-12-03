using System;
using System.IO;
using UnityEngine;

public class Config
{
    private static ConfigData _data;
    public static ConfigData Data
    {
        get
        {
            if (_data == null) LoadConfig();
            return _data;
        }
    }

    public static void SaveConfig()
    {
        //Convert the ConfigData object to a JSON string.
        string json = JsonUtility.ToJson(_data);

        //Write the JSON string to a file on disk.
        File.WriteAllText("config.json", json);
    }

    public static void LoadConfig()
    {
        //Get the JSON string from the file on disk.
        if (!File.Exists("config.json")) throw new Exception("Could not find config");
        string savedJson = File.ReadAllText("config.json");

        //Convert the JSON string back to a ConfigData object.
        _data = JsonUtility.FromJson<ConfigData>(savedJson);
    }
}


[Serializable]
public sealed class ConfigData
{
    public bool AlwaysAllowSlicing = true;
    public int MissPenalty = -10;
    public int MaxHitPoints = 100;

    public float SliceableFlightOffset = -1.8f;

    public MidiParsingConfig MidiParsing = new();
    public UserConfig User = new();

    [Serializable]
    public sealed class MidiParsingConfig
    {
        public string LeftSpawnTrackName = "beatninja-spawn-left";
        public int LeftSpawnOffset = 96;      // C7 Note

        public string RightSpawnTrackName = "beatninja-spawn-right";
        public int RightSpawnOffset = 101;    // F7 Note

        public int SpawnNote = 0;
        public int SpawnDoubleNote = 1;
        public int SpawnSpamNote = 2;

    }

    [Serializable]
    public sealed class UserConfig
    {
        public float LatencyOffset = 0f;
    }
}
