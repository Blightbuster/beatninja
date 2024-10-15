using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceAtCamera : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
