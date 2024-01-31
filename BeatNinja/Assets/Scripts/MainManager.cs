using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    private static MainManager _instance;
    public static MainManager Instance => _instance ??= Create();

    public GameData ActiveSongGameData = new GameData()
    {
        SongName = "BabyYoda"
    };

    public SongManager SongManager;

    private static MainManager Create()
    {
        var go = new GameObject("Managers");
        DontDestroyOnLoad(go);
        var main = go.AddComponent<MainManager>();
        main.SongManager = go.AddComponent<SongManager>();
        return main;
    }
}
