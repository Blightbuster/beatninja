using UnityEngine;

public class SkinShop : MonoBehaviour
{
    public GameObject confirmBut;
    public GameObject screen0;
    public GameObject screen1;
    public GameObject screen2;
    public GameObject screen3;
    public int flag = 0;

    public void Confirm()
    {
        screen0.SetActive(flag == 0);
        screen1.SetActive(flag == 1);
        screen2.SetActive(flag == 2);
        screen3.SetActive(flag == 3);
    }
}
