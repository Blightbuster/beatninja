using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject TabMenuEasy;
    public GameObject TabMenuMedium;
    public GameObject TabMenuHard;
    public GameObject ScrollEasy;
    public GameObject ScrollMedium;
    public GameObject ScrollHard;

    public void DefaultSongSelectMenu()
    {
        TabMenuEasy.SetActive(false);
        TabMenuMedium.SetActive(false);
        TabMenuHard.SetActive(false);
        ScrollEasy.SetActive(false);
        ScrollMedium.SetActive(false);
        ScrollHard.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
