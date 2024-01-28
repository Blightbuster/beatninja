using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
        NewSong();
    }

    private void Update()
    {
        ProcessSongEvents();
    }

    private void ProcessSongEvents()
    {
        if (ActiveSong == null) return;
        if (ActiveSong.Events.Count == 0)
        {
            ActiveSong = null;
            Invoke(nameof(EndSong), LastSongEvent.Duration + 3);
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
            _gameData.MaxScore += Config.Data.MaxHitPoints;
            if (spawnEvent.Side == SpawnerSide.Left) LeftSpawner.Spawn(spawnEvent);
            if (spawnEvent.Side == SpawnerSide.Right) RightSpawner.Spawn(spawnEvent);
        }
    }

    private void NewSong()
    {
        Time.timeScale = 1f;

        ClearScene();

        _blade.enabled = true;

        ScoreText.text = 0.ToString();
        ActiveSong = _songManager.Songs.Where(s => s.Name == _gameData.SongName).FirstOrDefault();
        if (ActiveSong == null) throw new Exception($"Song could not be found {_gameData.SongName}");

        SongSource.clip = ActiveSong.Audio;
        SongSource.Play();
    }

    private void EndSong()
    {
        ActiveSong = null;
        _gameData.Finished = true;
        SongSource.Stop();
        ClearScene();
        Invoke(nameof(LoadSongEndMenu), 3);
    }

    private void LoadSongEndMenu()
    {
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
        if (points > 0) CameraStressReceiver.InduceStress(points * 0.001f);
        if (points < 0) _gameData.AirStrikes += 1;
        _gameData.Score += points;
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
}
