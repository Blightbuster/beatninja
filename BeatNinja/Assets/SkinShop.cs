using nuitrack.NativeResources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkinShop : MonoBehaviour
{
    public GameObject confirmBut;
    public GameObject screen1;
    public GameObject screen2;
    public GameObject screen3;
    public GameObject screen4;
    public int flag = 0;

    
 

    public void Confirm()
    {
        
        
        if (flag == 1)
        {

            screen1.SetActive(true);
            screen2.SetActive(false);
            screen3.SetActive(false);
            screen4.SetActive(false);
            
        }
        else if (flag == 2)
        {
            screen1.SetActive(false);
            screen2.SetActive(true);
            screen3.SetActive(false);
            screen4.SetActive(false);
            
        }
        else if (flag == 3)
        {
            screen1.SetActive(false);
            screen2.SetActive(false);
            screen3.SetActive(true);
            screen4.SetActive(false);
            
        }
        else if (flag == 4)
        {
            screen1.SetActive(false);
            screen2.SetActive(false);
            screen3.SetActive(false);
            screen4.SetActive(true);
            
        }
        //Debug.Log(SceneManager.GetActiveScene().name); //"Beatninja"
        





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

    public void select5() {
        flag = 5;
        Debug.Log("flag = 5");
    }

    public void select6()
    {
        flag = 6;
        Debug.Log("flag = 6");
    }

    public void select7()
    {
        flag = 7;
        Debug.Log("flag = 7");
    }
}
