using System.Globalization;
using UnityEngine;

public static class Util
{
    public static Vector2 AngleToDir(float angle) => new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    public static float DirToAngle(Vector2 dir) => Mathf.Atan2(dir.y, dir.x);

    public static string GroupInt(this int value)
    {
        var ni = new CultureInfo(CultureInfo.CurrentCulture.Name).NumberFormat;
        ni.NumberGroupSeparator = " ";
        ni.NumberGroupSizes = new int[] { 3 };
        return value.ToString(ni);
    }
}
