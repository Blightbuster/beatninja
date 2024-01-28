using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        ScoreText.text = _gameData.Score.ToString();
        CoinsText.text = _gameData.CoinsAwarded.ToString();

        Star1.SetActive(_gameData.Rating >= 1);
        Star2.SetActive(_gameData.Rating >= 2);
        Star3.SetActive(_gameData.Rating >= 3);

        Config.Data.Progress.Coins += _gameData.CoinsAwarded;
        Config.Data.Progress.Scores[_gameData.SongName] = _gameData;
        Config.SaveConfig();
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
