using nuitrack.NativeResources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkinShop : MonoBehaviour
{
    public GameObject ssButton;
    public GameObject ssScreen;
    public GameObject confirmBut;
    public GameObject cancelBut;
    public GameObject screen1;
    public GameObject screen2;
    public GameObject screen3;
    public GameObject screen4;
    public int flag = 0;

    
    public void openSS()
    {
        ssScreen.SetActive(true);
        ssButton.SetActive(false);
        Time.timeScale = (0); //pause the game
    }

    public void Confirm()
    {
        ssScreen.SetActive(false);
        ssButton.SetActive(true);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);//restart the game
        if (flag == 1)
        {

            screen1.SetActive(true);
            screen2.SetActive(false);
            screen3.SetActive(false);
            screen4.SetActive(false);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);//restart the game
        }
        else if (flag == 2)
        {
            screen1.SetActive(false);
            screen2.SetActive(true);
            screen3.SetActive(false);
            screen4.SetActive(false);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);//restart the game
        }
        else if (flag == 3)
        {
            screen1.SetActive(false);
            screen2.SetActive(false);
            screen3.SetActive(true);
            screen4.SetActive(false);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);//restart the game
        }
        else if (flag == 4)
        {
            screen1.SetActive(false);
            screen2.SetActive(false);
            screen3.SetActive(false);
            screen4.SetActive(true);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);//restart the game
        }
        //Debug.Log(SceneManager.GetActiveScene().name);//"Beatninja"
        Time.timeScale = (1);





    }

    

    public void Cancel() {

        Time.timeScale = (1);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);//restart the game
    }


    public void select1() {
        flag = 1;
        Debug.Log("falg = 1");
    }

    public void select2() {
        flag = 2;
        Debug.Log("falg = 2");
    }

    public void select3() {
        flag = 3;
        Debug.Log("falg = 3");
    }

    public void select4() {       
        flag = 4;
        Debug.Log("falg = 4");
    }

}
