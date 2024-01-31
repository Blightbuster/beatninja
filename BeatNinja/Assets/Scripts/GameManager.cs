using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<Transform> BackgroundSkins;
    public List<GameObject> CharacterSkins;

    public List<GameObject> StreakStages;

    public GameObject LoadingGo;

    public FreezeTransform CharacterTransformFreezer;
    public FreezeTransform GameRootTransformFreezer;

    public Transform GameRoot;

    private SongManager _songManager => MainManager.Instance.SongManager;

    public AudioSource SongSource;
    public float SongTime => SongSource.time;
    private SongEvent LastSongEvent;

    public Text ScoreText;
    public Image FadeImage;

    private Blade _blade;

    public Spawner LeftSpawner;
    public Spawner RightSpawner;

    public SliceArea LeftSliceArea;
    public SliceArea RightSliceArea;

    public StressReceiver CameraStressReceiver;

    public Song ActiveSong;

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
        LoadSong();
        Invoke(nameof(DelayedStart), 3f);
    }

    private void DelayedStart()
    {
        LoadingGo.SetActive(false);
        StartSong();
    }

    private void Update()
    { 
        UpdateStreakStageFX();
        ProcessSongEvents();
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

    private void ProcessSongEvents()
    {
        if (ActiveSong == null) return;
        if (ActiveSong.Events.Count == 0)
        {
            ActiveSong = null;
            Invoke(nameof(EndSong), (LastSongEvent?.Duration ?? 0) + 3);
            return;
        }

        var nextEventTime = ActiveSong.Events.Peek().SpawnTime;
        var totalOffset = Config.Data.User.LatencyOffset + Config.Data.SliceableFlightOffset;
        if (SongTime > (nextEventTime + totalOffset))
        {
            ExecuteSongEvent(ActiveSong.Events.Dequeue());
            // Recursive call to execute events which might happen at the same exact time
            ProcessSongEvents();
        }
    }

    private void ExecuteSongEvent(SongEvent e)
    {
        LastSongEvent = e;
        if (e is SpawnEvent spawnEvent)
        {
            _gameData.MaxScore += (int)(Config.Data.MaxHitPoints * 2.8f);
            if (spawnEvent is SpawnSpamNoteEvent) _gameData.MaxScore += (int)(Config.Data.MaxHitPoints * 2.8f * 4f);
            if (spawnEvent.Side == SpawnerSide.Left) LeftSpawner.Spawn(spawnEvent);
            if (spawnEvent.Side == SpawnerSide.Right) RightSpawner.Spawn(spawnEvent);
        }
    }

    private void LoadSong()
    {
        Time.timeScale = 1f;

        ClearScene();

        _blade.enabled = true;

        ScoreText.text = 0.ToString();
        ActiveSong = _songManager.Songs.Where(s => s.Name == _gameData.SongName).FirstOrDefault();
        if (ActiveSong == null) throw new Exception($"Song could not be found {_gameData.SongName}");

        SongSource.clip = ActiveSong.Audio;
    }

    private void StartSong() {

        SongSource.Play();
    }

    private void EndSong()
    {
        Debug.Log("EndSong called!");
        ActiveSong = null;
        _gameData.Finished = true;
        SongSource.Stop();
        ClearScene();
        Invoke(nameof(LoadSongEndMenu), 3);
    }

    private void LoadSongEndMenu()
    {
        Debug.Log("LoadSongEndMenu called!");
        SceneManager.LoadScene("SongEndMenu");
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void ClearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();

        foreach (Fruit fruit in fruits)
        {
            Destroy(fruit.gameObject);
        }
    }

    public void IncreaseScore(int points)
    {
        if (points > 0)
        {
            CameraStressReceiver.InduceStress(points * 0.001f);
            _gameData.Streak++;
        }
        if (points < 0)
        {
            _gameData.AirStrikes += 1;
            _gameData.Streak = 0;
        }
        _gameData.Score += (int)((float)points * _gameData.StreakMultiplier);
        ScoreText.text = _gameData.Score.ToString();
    }

    public void LeftSlice()
    {
        var points = LeftSliceArea.Slice();
        IncreaseScore((int)points);
    }

    public void RightSlice()
    {
        var points = RightSliceArea.Slice();
        IncreaseScore((int)points);
    }

    public void UpdateStreakStageFX()
    {
        for (var i = 0; i < StreakStages.Count; i++) StreakStages[i].SetActive(_gameData.StreakLevel > i);
    }
}
