using System.Collections;
using System.Collections.Generic;
// using System.Diagnostics;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    { 
        if (collision.gameObject.GetComponent<Sliceable>() != null)
        {
            Sliceable fruit = collision.gameObject.GetComponent<Sliceable>();
            Vector3 point = collision.GetContact(0).point;
            fruit.Slice(new Vector2(-point[0], -point[1]));
            Debug.Log(point[0]);
        }
    }
}
