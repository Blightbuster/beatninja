using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCharacter : MonoBehaviour
{
    public GameObject screen1;
    public GameObject screen2;
    public GameObject screen3;
    public GameObject screen4;
    public GameObject screen5;
    public GameObject screen6;
    public GameObject screen7;
    //public int total = 7;
    


    // Start is called before the first frame update
    void Start()
    {
        int character = PlayerPrefs.GetInt("screen");


        if (character == 1)
        {
            screen1.SetActive(true);
            screen2.SetActive(false);
            screen3.SetActive(false);
            screen4.SetActive(false);
            screen5.SetActive(false);
            screen6.SetActive(false);
            screen7.SetActive(false);

            Debug.Log("The user has chosen character " + character);
        }
        else if (character == 2)
        {
            screen1.SetActive(false);
            screen2.SetActive(true);
            screen3.SetActive(false);
            screen4.SetActive(false);
            screen5.SetActive(false);
            screen6.SetActive(false);
            screen7.SetActive(false);

            Debug.Log("The user has chosen character " + character);
        }
        else if (character == 3)
        {
            screen1.SetActive(false);
            screen2.SetActive(false);
            screen3.SetActive(true);
            screen4.SetActive(false);
            screen5.SetActive(false);
            screen6.SetActive(false);
            screen7.SetActive(false);

            Debug.Log("The user has chosen character " + character);
        }
        else if (character == 4)
        {
            screen1.SetActive(false);
            screen2.SetActive(false);
            screen3.SetActive(false);
            screen4.SetActive(true);
            screen5.SetActive(false);
            screen6.SetActive(false);
            screen7.SetActive(false);

            Debug.Log("The user has chosen character " + character);
        }
        else if (character == 5)
        {
            screen1.SetActive(false);
            screen2.SetActive(false);
            screen3.SetActive(false);
            screen4.SetActive(false);
            screen5.SetActive(true);
            screen6.SetActive(false);
            screen7.SetActive(false);

            Debug.Log("The user has chosen character " + character);
        }
        else if (character == 6)
        {
            screen1.SetActive(false);
            screen2.SetActive(false);
            screen3.SetActive(false);
            screen4.SetActive(false);
            screen5.SetActive(false);
            screen6.SetActive(true);
            screen7.SetActive(false);

            Debug.Log("The user has chosen character " + character);
        }
        else if (character == 7)
        {
            screen1.SetActive(false);
            screen2.SetActive(false);
            screen3.SetActive(false);
            screen4.SetActive(false);
            screen5.SetActive(false);
            screen6.SetActive(false);
            screen7.SetActive(true);

            Debug.Log("The user has chosen character " + character);
        }
        else
        {
            Debug.Log("It's default character");
        }
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
}
