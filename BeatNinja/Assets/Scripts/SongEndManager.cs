using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SongEndManager : MonoBehaviour
{
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Rating;
    public TextMeshProUGUI PassedFailedText;

    private GameData _gameData => GameManager.Instance.ActiveSongGameData;

    void Start()
    {
        Score.text = _gameData.Score.ToString();
        Rating.text = _gameData.Rating.ToString();
        PassedFailedText.text = _gameData.Finished ? "COMPLETED" : "FAILED";
    }

    void Update()
    {
        
    }
}
