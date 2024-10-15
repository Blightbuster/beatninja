using Force.DeepCloner;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerLatencyTest : GameManager
{
    public static new GameManagerLatencyTest Instance;

    private SongManager _songManager => MainManager.Instance.SongManager;

    private Blade _blade;

    public AudioClip BeatSound;

    private GameData _gameData => MainManager.Instance.ActiveSongGameData;

    private void Awake()
    {
        Instance = this;
        GameManager._instance = null;
        _blade = FindObjectOfType<Blade>();
    }

    private void Start()
    {
        LoadingGo.SetActive(true);
        LoadSelectedBackgroundSkin();
        LoadSelectedCharacterSkin();
        CharacterTransformFreezer.Freeze();
        GameRootTransformFreezer.Freeze();
        LoadingGo.SetActive(false);

        InvokeRepeating(nameof(NextBeat), 3f, 1f);
    }

    private void Update()
    {
    }

    private void LoadSelectedBackgroundSkin()
    {
        var transform = BackgroundSkins[Config.Data.Progress.SelectedBackgroundSkin];
        GameRoot.position = transform.position;
        GameRoot.rotation = transform.rotation;
    }

    private void LoadSelectedCharacterSkin()
    {
        for (var i = 0; i < CharacterSkins.Count; i++) CharacterSkins[i].SetActive(i == Config.Data.Progress.SelectedCharacterSkin);
    }

    private void NextBeat()
    {
        LeftSpawner.Spawn(new SpawnNoteEvent() { SpawnTime = 0, HitsNeeded = 1, Duration = 0.1f, Side = SpawnerSide.Left });
        RightSpawner.Spawn(new SpawnNoteEvent() { SpawnTime = 0, HitsNeeded = 1, Duration = 0.1f, Side = SpawnerSide.Right });
        Invoke(nameof(PlayBeatOnce), -Config.Data.User.LatencyOffset + -Config.Data.SliceableFlightOffset - 0.05f);
    }

    private void PlayBeatOnce() => AudioSource.PlayClipAtPoint(BeatSound, Camera.main.transform.position, 0.5f);


    public new void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public new void LeftSlice()
    {
        var points = LeftSliceArea.Slice();
    }

    public new void RightSlice()
    {
        var points = RightSliceArea.Slice();
    }
}
