using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class CoinDisplay : MonoBehaviour
{
    public TextMeshProUGUI Text;
    void Update()
    {
        Text.text = Config.Data.Progress.Coins.GroupInt();
    }
}
