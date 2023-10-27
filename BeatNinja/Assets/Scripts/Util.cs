using UnityEngine;

public static class Util
{
    public static Vector2 AngleToDir(float angle) => new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    public static float DirToAngle(Vector2 dir) => Mathf.Atan2(dir.y, dir.x);
}
