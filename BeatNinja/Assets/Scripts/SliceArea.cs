using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class SliceArea : MonoBehaviour
{
    public Blade Blade;

    private CircleCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        if (_collider.offset != Vector2.zero) Debug.LogError("The SliceArea's collider must not use an offset!");
    }

    public float Slice()
    {
        var dir = Random.onUnitSphere;
        if (dir.y < 0) dir.y *= -1;
        if(!Blade.Slice(dir)) return 0;

        var sliceables = FindObjectsByType<Sliceable>(FindObjectsSortMode.None);

        // Since we dont care about the z-axis transmuting is fine here
        var ordered = sliceables.OrderBy(x => Vector2.Distance(x.transform.position, this.transform.position));
        var nearest = ordered.FirstOrDefault(); // Default is null
        if (nearest == null) return Config.MissPenalty;
        if (!_collider.OverlapPoint(nearest.transform.position)) return Config.MissPenalty;

        var distance = Vector2.Distance(this.transform.position, nearest.transform.position);
        var nDistance = distance / _collider.radius;

        return nearest.Slice(dir) + (500f * (1f - nDistance));
    }
}
