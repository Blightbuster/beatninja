using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private ScoreData sd;
    private void Awake()
    {
        //if active load the data from prious save
        //var json = PlayerPrefs.GetString("scores", "{}");
        sd = new ScoreData();
        //sd = JsonUtility.FromJson<ScoreData>(json);
    }

    public IEnumerable<Score> GetHighScores()
    {
        return sd.scores.OrderByDescending(keySelector: x => x.score); 
    }

    public void Addscore(Score score)
    {
        sd.scores.Add(score);
    }

    //for loading saved score
    private void OnDestroy()
    {
        SaveScore();
    }

    public void SaveScore()
    {
        var json = JsonUtility.ToJson(sd);
        PlayerPrefs.SetString("score", json);
    }
}
