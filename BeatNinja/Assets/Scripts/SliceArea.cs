using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class SliceArea : MonoBehaviour
{
    public Blade Blade;
    public AudioClip SliceSound;
    public GameObject PointsPopUpPrefab;

    private CircleCollider2D _collider;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_collider.offset != Vector2.zero) Debug.LogError("The SliceArea's collider must not use an offset!");
    }

    private void FixedUpdate()
    {
        var nearest = GetSliceableInArea();
        if (nearest.sliceable == null)
        {
            _spriteRenderer.color = Color.white;
            return;
        }
        _spriteRenderer.color = Color.red;
    }

    private (Sliceable? sliceable, float distance) GetSliceableInArea()
    {
        var cam = Camera.main.transform;
        Debug.DrawRay(cam.position, _collider.transform.position - cam.position, Color.red);
        var ray = new Ray(cam.position, (_collider.transform.position - cam.position).normalized);
        //if(!Physics.Raycast(ray, out var hitInfo, 200, 1 << LayerMask.NameToLayer("Sliceable"))) return null;
        if (!Physics.SphereCast(ray, _collider.radius, out var hitInfo, 200, 1 << LayerMask.NameToLayer("Sliceable"))) return (null, 0);
        return (hitInfo.transform.GetComponent<Sliceable>(), Vector3.Cross(ray.direction, hitInfo.point - ray.origin).magnitude);
    }

    public float Slice()
    {
        var points = SliceInner();
        var popUp = Instantiate(PointsPopUpPrefab, this.transform.position, Quaternion.identity);
        popUp.GetComponentInChildren<TextPopUp>().SetText(((int)points).ToString());
        if (points > 0) AudioSource.PlayClipAtPoint(SliceSound, Camera.main.transform.position, 0.05f);
        return points;
    }

    private float SliceInner()
    {
        var dir = Random.onUnitSphere;
        if (dir.y < 0) dir.y *= -1;
        if (!Blade.Slice(dir)) return 0;

        var nearest = GetSliceableInArea();
        if (nearest.sliceable == null) return Config.Data.MissPenalty;

        var nDistance = nearest.distance / _collider.radius;
        return nearest.sliceable.Slice(dir) * (1f - nDistance);
    }
}
