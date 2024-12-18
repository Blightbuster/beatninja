using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SongEndManager : MonoBehaviour
{
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI CoinsText;

    private GameData _gameData => MainManager.Instance.ActiveSongGameData;

    void Start()
    {
        ScoreText.text = _gameData.Finished ? _gameData.Score.ToString() : "FAILED";
        CoinsText.text = _gameData.CoinsAwarded.ToString();

        Star1.SetActive(_gameData.Rating >= 1);
        Star2.SetActive(_gameData.Rating >= 2);
        Star3.SetActive(_gameData.Rating >= 3);

        Config.Data.Progress.Coins += _gameData.CoinsAwarded;
        var exists = Config.Data.Progress.Scores.TryGetValue(_gameData.SongName, out var save);
        if ((!exists || _gameData.Percentage > save.Percentage) && _gameData.Finished) Config.Data.Progress.Scores[_gameData.SongName] = _gameData;
        Config.SaveConfig();
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
