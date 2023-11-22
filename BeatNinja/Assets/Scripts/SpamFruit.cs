using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class SpamFruit : Sliceable
{
    private Rigidbody _fruitRigidbody;
    private Collider _fruitCollider;
    private float _firstHitTime = float.MaxValue;

    private void Start()
    {
        SanityCheck();
        _fruitRigidbody = GetComponent<Rigidbody>();
        _fruitCollider = GetComponent<Collider>();
    }

    protected new void SanityCheck()
    {
        base.SanityCheck();
    }

    private void Update()
    {
        // Make it explode once its set duration expires after it has been hit once
        if (GameManager.Instance.SongTime > _firstHitTime + EventOrigin.Duration) Explode();
    }

    public override float Slice(Vector2 dir)
    {
        if (_firstHitTime == float.MaxValue) _firstHitTime = GameManager.Instance.SongTime;
        SliceEffect.Play();
        _fruitRigidbody.isKinematic = true;
        return Config.MaxHitPoints;
    }

    private float Explode()
    {
        IsSliced = true;

        // Disable the whole fruit
        _fruitCollider.enabled = false;
        WholeObject.SetActive(false);

        // Enable the sliced fruit
        SlicedObject.SetActive(true);

        // Rotate based on the slice angle
        Vector3 direction = this._fruitRigidbody.velocity;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        SlicedObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // TODO GetOrAddComponent is bugged?
        // Perhaps it needs a update cycle to add the component?
        var slices = new List<Rigidbody>();
        foreach (Transform t in SlicedObject.transform) slices.Add(t.gameObject.GetOrAddComponent<Rigidbody>());

        // Add a force to each slice based on the blade direction
        foreach (Rigidbody slice in slices)
        {
            slice.velocity = _fruitRigidbody.velocity;
            slice.AddForceAtPosition(Vector2.up * 2, Vector2.up, ForceMode.Impulse);
        }
        return Config.MaxHitPoints;
    }
}
