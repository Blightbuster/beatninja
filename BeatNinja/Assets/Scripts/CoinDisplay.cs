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
        var ni = new CultureInfo(CultureInfo.CurrentCulture.Name).NumberFormat;
        ni.NumberGroupSeparator = " ";
        ni.NumberGroupSizes = new int[] { 3 };
        Text.text = Config.Data.Progress.Coins.ToString(ni);
    }
}
