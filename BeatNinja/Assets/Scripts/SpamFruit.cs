using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class SpamFruit : Sliceable
{
    public GameObject WholeObject;
    public GameObject SlicedObject;

    private Rigidbody _fruitRigidbody;
    private Collider _fruitCollider;
    private ParticleSystem _juiceEffect;
    private float _firstHitTime = float.MaxValue;

    public float Duration;

    private void Awake()
    {
        _fruitRigidbody = GetComponent<Rigidbody>();
        _fruitCollider = GetComponent<Collider>();
        _juiceEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (GameManager.Instance.SongTime > _firstHitTime + Duration) Explode();
    }

    public override float Slice(Vector2 dir)
    {
        if (_firstHitTime == float.MaxValue) _firstHitTime = GameManager.Instance.SongTime;
        _juiceEffect.Play();
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

        Rigidbody[] slices = SlicedObject.GetComponentsInChildren<Rigidbody>();

        // Add a force to each slice based on the blade direction
        foreach (Rigidbody slice in slices)
        {
            slice.velocity = _fruitRigidbody.velocity;
            slice.AddForceAtPosition(Vector2.up * 2, Vector2.up, ForceMode.Impulse);
        }
        return Config.MaxHitPoints;
    }
}
