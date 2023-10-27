using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(Collider))]
public class Fruit : Sliceable
{
    public GameObject Whole;
    public GameObject Sliced;

    private Rigidbody _fruitRigidbody;
    private Collider _fruitCollider;
    private ParticleSystem _juiceEffect;

    public int Points;

    private void Awake()
    {
        _fruitRigidbody = GetComponent<Rigidbody>();
        _fruitCollider = GetComponent<Collider>();
        _juiceEffect = GetComponentInChildren<ParticleSystem>();
    }

    public override float Slice(Vector2 dir)
    {
        Vector3 direction = this._fruitRigidbody.velocity;

        // Disable the whole fruit
        _fruitCollider.enabled = false;
        Whole.SetActive(false);

        // Enable the sliced fruit
        Sliced.SetActive(true);
        _juiceEffect.Play();

        // Rotate based on the slice angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody[] slices = Sliced.GetComponentsInChildren<Rigidbody>();

        // Add a force to each slice based on the blade direction
        foreach (Rigidbody slice in slices)
        {
            slice.velocity = _fruitRigidbody.velocity;
            slice.AddForceAtPosition(dir * 2, dir, ForceMode.Impulse);
        }
        return Config.MaxHitPoints;
    }
}
