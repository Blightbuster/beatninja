using JetBrains.Rider.Unity.Editor;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Fruit : Sliceable
{
    private Rigidbody _fruitRigidbody;
    private Collider _fruitCollider;

    private int _hitCount = 0;
    public int HitsNeeded = 1;

    private void Start()
    {
        SanityCheck();
        _fruitRigidbody = GetComponent<Rigidbody>();
        _fruitCollider = GetComponent<Collider>();
        HitsNeeded = ((SpawnNoteEvent)EventOrigin).HitsNeeded;
    }

    protected new void SanityCheck()
    {
        base.SanityCheck();
    }

    public override float Slice(Vector2 dir)
    {
        _hitCount++;
        SliceEffect.Play();

        if (HitsNeeded == _hitCount) return FinalSlice(dir);
        return Config.Data.MaxHitPoints;
    }

    private float FinalSlice(Vector2 dir)
    {
        IsSliced = true;

        // Disable the whole fruit
        _fruitCollider.enabled = false;
        WholeObject.SetActive(false);

        // Enable the sliced fruit
        SlicedObject.SetActive(true);

        // Rotate based on the slice angle
        var rb = this._fruitRigidbody.GetComponent<Rigidbody>();
        rb.velocity += new Vector3(dir.x, dir.y, 0) * -4f;
        HandleSlicedPieces();
        return Config.Data.MaxHitPoints;
    }

    private void HandleSlicedPieces()
    {
        GameObject top = null;
        GameObject bottom = null;
        foreach (Transform t in SlicedObject.transform)
        {
            if (t.name.EndsWith("Top")) top = t.gameObject;
            if (t.name.EndsWith("Bottom")) bottom = t.gameObject;
            t.AddComponent<Rigidbody>();
            t.AddComponent<SphereCollider>();
            t.GetComponent<Rigidbody>().velocity = this._fruitRigidbody.velocity;
        }

        top.GetComponent<Rigidbody>().AddForce(Vector3.up*50);
        bottom.GetComponent<Rigidbody>().AddForce(Vector3.down*50);
    }
}
