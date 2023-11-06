using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sliceable : MonoBehaviour
{
    public bool IsSliced = false;

    /// <summary>
    /// Slices the object
    /// </summary>
    /// <param name="dir">Normalized angle of the slicing operation</param>
    /// <returns>Points for slicing the object</returns>
    public abstract float Slice(Vector2 dir);
}
