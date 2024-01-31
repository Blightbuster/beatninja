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
    public static GameManagerLatencyTest Instance;

    public List<Transform> BackgroundSkins;
    public List<GameObject> CharacterSkins;

    public List<GameObject> StreakStages;

    public GameObject LoadingGo;

    public FreezeTransform CharacterTransformFreezer;
    public FreezeTransform GameRootTransformFreezer;

    public Transform GameRoot;

    private SongManager _songManager => MainManager.Instance.SongManager;

    public Text ScoreText;
    public Image FadeImage;

    private Blade _blade;

    public Spawner LeftSpawner;
    public Spawner RightSpawner;

    public SliceArea LeftSliceArea;
    public SliceArea RightSliceArea;

    public StressReceiver CameraStressReceiver;

    public AudioClip BeatSound;

    private GameData _gameData => MainManager.Instance.ActiveSongGameData;

    private void Awake()
    {
        Instance = this;
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


    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LeftSlice()
    {
        var points = LeftSliceArea.Slice();
    }

    public void RightSlice()
    {
        var points = RightSliceArea.Slice();
    }
}
