using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelect : MonoBehaviour
{
    public GameObject confirmBut;
    public int flag = 0;




    public void Confirm()
    {
        /* if (flag != 0)
         {
             PlayerPrefs.SetInt("screen" + flag, flag);

         }
         else { Debug.Log("Didn't choose skin"); }*/

        if (flag == 1)
        {

            PlayerPrefs.SetInt("screen", 1);

        }
        else if (flag == 2)
        {
            PlayerPrefs.SetInt("screen", 2);

        }
        else if (flag == 3)
        {
            PlayerPrefs.SetInt("screen", 3);

        }
        else if (flag == 4)
        {
            PlayerPrefs.SetInt("screen", 4);

        }
        else if (flag == 5)
        {
            PlayerPrefs.SetInt("screen", 5);
        }
        else if (flag == 6)
        {
            PlayerPrefs.SetInt("screen", 6);
        }
        else if (flag == 7)
        {
            PlayerPrefs.SetInt("screen", 7);
        }
        else { Debug.Log("Didn't choose skin"); }

        Debug.Log("character " + flag + " has been chosen");






    }






    public void select1()
    {
        flag = 1;
        Debug.Log("falg = 1");
    }

    public void select2()
    {
        flag = 2;
        Debug.Log("falg = 2");
    }

    public void select3()
    {
        flag = 3;
        Debug.Log("falg = 3");
    }

    public void select4()
    {
        flag = 4;
        Debug.Log("falg = 4");
    }

    public void select5()
    {
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


