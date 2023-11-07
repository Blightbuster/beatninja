using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Fruit : Sliceable
{
    public GameObject WholeObject;
    public GameObject SlicedObject;

    private Rigidbody _fruitRigidbody;
    private Collider _fruitCollider;
    private ParticleSystem _juiceEffect;

    private int _hitCount = 0;
    public int HitsNeeded = 1;

    private void Start()
    {
        _fruitRigidbody = GetComponent<Rigidbody>();
        _fruitCollider = GetComponent<Collider>();
        _juiceEffect = GetComponentInChildren<ParticleSystem>();
        HitsNeeded = ((SpawnNoteEvent)EventOrigin).HitsNeeded;
    }

    public override float Slice(Vector2 dir)
    {
        _hitCount++;
        _juiceEffect.Play();

        if (HitsNeeded == _hitCount) return FinalSlice(dir);
        return Config.MaxHitPoints;
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
        Vector3 direction = this._fruitRigidbody.velocity;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        SlicedObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody[] slices = SlicedObject.GetComponentsInChildren<Rigidbody>();

        // Add a force to each slice based on the blade direction
        foreach (Rigidbody slice in slices)
        {
            slice.velocity = _fruitRigidbody.velocity;
            slice.AddForceAtPosition(dir * 2, dir, ForceMode.Impulse);
        }
        return Config.MaxHitPoints;
    }
}
