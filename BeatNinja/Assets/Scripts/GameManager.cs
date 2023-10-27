using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public SongManager SongManager;

    public Text ScoreText;
    public Image FadeImage;

    private Blade _blade;

    public Spawner LeftSpawner;
    public Spawner RightSpawner;

    public SliceArea LeftSliceArea;
    public SliceArea RightSliceArea;

    public float SongOffset;

    private int _score;
    private Song _activeSong;
    private float _startTime;

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
        var timeSinceStart = Time.time - _startTime;
        var nextEventTime = _activeSong.Events.Peek().Time;
        var totalOffset = SongOffset + Config.SliceableFlightOffset;
        while (timeSinceStart > (nextEventTime + totalOffset))
        {
            ExecuteSongEvent(_activeSong.Events.Dequeue());
            nextEventTime = _activeSong.Events.Peek().Time;
        }
    }

    private void ExecuteSongEvent(SongEvent e)
    {
        if (e is LeftSpawnEvent) LeftSpawner.Spawn();
        if (e is RightSpawnEvent) RightSpawner.Spawn();
    }

    private void NewGame()
    {
        Time.timeScale = 1f;

        ClearScene();

        _blade.enabled = true;

        _score = 0;
        ScoreText.text = _score.ToString();
        _activeSong = SongManager.Songs[0];
        _startTime = Time.time;
        AudioSource.PlayClipAtPoint(_activeSong.Audio, Camera.main.transform.position);
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
