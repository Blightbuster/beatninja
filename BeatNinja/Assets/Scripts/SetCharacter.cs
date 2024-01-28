using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCharacter : MonoBehaviour
{
    public GameObject screen0;
    public GameObject screen1;
    public GameObject screen2;
    public GameObject screen3;
    public GameObject screen4;
    public GameObject screen5;
    public GameObject screen6;

    // Start is called before the first frame update
    void Start()
    {
        int character = Config.Data.Progress.Character;
        screen0.SetActive(character == 0);
        screen1.SetActive(character == 1);
        screen2.SetActive(character == 2);
        screen3.SetActive(character == 3);
        screen4.SetActive(character == 4);
        screen5.SetActive(character == 5);
        screen6.SetActive(character == 6);
    }
}
