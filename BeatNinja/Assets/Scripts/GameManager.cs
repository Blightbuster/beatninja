using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public SongManager SongManager;

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

    public GameData ActiveSongGameData;
    public Song ActiveSong;

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
            if (spawnEvent.Side == SpawnerSide.Left) LeftSpawner.Spawn(spawnEvent);
            if (spawnEvent.Side == SpawnerSide.Right) RightSpawner.Spawn(spawnEvent);
        }
    }

    private void NewSong()
    {
        Time.timeScale = 1f;

        ClearScene();

        _blade.enabled = true;

        ActiveSongGameData = new();
        ScoreText.text = 0.ToString();
        ActiveSong = SongManager.Songs[0];

        SongSource.clip = ActiveSong.Audio;
        SongSource.Play();
    }

    private void EndSong()
    {
        ActiveSong = null;
        ActiveSongGameData.Finished = true;
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
        if (points < 0) ActiveSongGameData.AirStrikes += 1;
        ActiveSongGameData.Score += points;
        ScoreText.text = ActiveSongGameData.Score.ToString();
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
