using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FreezeTransform : MonoBehaviour
{
    private Vector3 _position;
    private Quaternion _rotation;
    private Vector3 _localScale;

    private bool _frozen = false;

    public void Freeze()
    {
        _position = this.transform.position;
        _rotation = this.transform.rotation;
        _localScale = this.transform.localScale;
        _frozen = true;
    }

    public void UnFreeze() => _frozen = false;

    void Update()
    {
        if (!_frozen) return;
        this.transform.position = _position;
        this.transform.rotation = _rotation;
        this.transform.localScale = _localScale;
    }
}
