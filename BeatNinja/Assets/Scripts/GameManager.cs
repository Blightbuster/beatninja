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

    private int _score;
    private Song _activeSong;

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
        if (_activeSong == null) return;
        if (_activeSong.Events.Count == 0)
        {
            _activeSong = null;
            Invoke(nameof(EndSong), LastSongEvent.Duration + 3);
            return;
        }

        var nextEventTime = _activeSong.Events.Peek().SpawnTime;
        var totalOffset = Config.Data.User.LatencyOffset + Config.Data.SliceableFlightOffset;
        if (SongTime > (nextEventTime + totalOffset))
        {
            ExecuteSongEvent(_activeSong.Events.Dequeue());
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

        _score = 0;
        ScoreText.text = _score.ToString();
        _activeSong = SongManager.Songs[0];

        SongSource.clip = _activeSong.Audio;
        SongSource.Play();
    }

    private void EndSong()
    {
        _activeSong = null;
        SongSource.Stop();
        ClearScene();
        Invoke(nameof(LoadMainMenu), 3);

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
        _score += points;
        ScoreText.text = _score.ToString();
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
