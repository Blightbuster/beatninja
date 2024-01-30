using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SongDisplay : MonoBehaviour
{
    public GameData GameData;

    public TextMeshProUGUI SongTitleText;
    public TextMeshProUGUI SongArtistText;
    public TextMeshProUGUI ScoreText;

    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;

    private Button _playButton;

    void Start()
    {
        _playButton = GetComponent<Button>();
        _playButton.onClick.AddListener(PlaySong);
        LoadGameDataFromConfig();
        Apply();
    }

    public void Apply()
    {
        SongTitleText.text = GameData.DisplayTitle;
        SongArtistText.text = GameData.DisplayArtist;
        ScoreText.text = GameData.Score.ToString();

        Star1.SetActive(GameData.Rating >= 1);
        Star2.SetActive(GameData.Rating >= 2);
        Star3.SetActive(GameData.Rating >= 3);
    }

    public void LoadGameDataFromConfig()
    {
        var data = Config.Data.Progress.Scores.Where(s => s.Key == GameData.SongName).FirstOrDefault();
        GameData = data.Value == null ? GameData.EmptyCopy() : data.Value;
    }

    public void PlaySong()
    {
        MainManager.Instance.ActiveSongGameData = GameData.EmptyCopy();
        SceneManager.LoadScene("BeatNinja");
    }
}
