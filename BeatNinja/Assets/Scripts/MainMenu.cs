using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameData ActiveSongGameData;

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
