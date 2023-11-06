using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public SongManager SongManager;

    public AudioSource SongSource;
    public float SongTime => SongSource.time;

    public Text ScoreText;
    public Image FadeImage;

    private Blade _blade;

    public Spawner LeftSpawner;
    public Spawner RightSpawner;

    public SliceArea LeftSliceArea;
    public SliceArea RightSliceArea;

    private int _score;
    private Song _activeSong;

    private void Awake()
    {
        Instance = this;
        _blade = FindObjectOfType<Blade>();
    }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        var nextEventTime = _activeSong.Events.Peek().Time;
        var totalOffset = Config.User.LatencyOffset + Config.SliceableFlightOffset;
        while (SongTime > (nextEventTime + totalOffset))
        {
            if (_activeSong.Events.Count == 0) break;
            ExecuteSongEvent(_activeSong.Events.Dequeue());
            nextEventTime = _activeSong.Events.Peek().Time;
        }
    }

    private void ExecuteSongEvent(SongEvent e)
    {
        if (e is SpawnEvent spawnEvent)
        {
            if (spawnEvent.Side == SpawnerSide.Left) LeftSpawner.Spawn(spawnEvent);
            if (spawnEvent.Side == SpawnerSide.Right) RightSpawner.Spawn(spawnEvent);
        }
    }

    private void NewGame()
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
