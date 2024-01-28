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
    public GameObject PointsPopUpPrefab;

    private CircleCollider2D _collider;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_collider.offset != Vector2.zero) Debug.LogError("The SliceArea's collider must not use an offset!");
    }

    private void Update()
    {
        var nearest = GetNearestSliceable();
        if (nearest == null) return;
        var distance = Vector2.Distance(this.transform.position, nearest.transform.position);
        var nDistance = distance / _collider.radius;
        if (nDistance < 0.5f) _spriteRenderer.color = Color.red;
        else _spriteRenderer.color = Color.white;
    }

    private Sliceable? GetNearestSliceable()
    {
        // Find all non-sliced sliceables
        var sliceables = FindObjectsByType<Sliceable>(FindObjectsSortMode.None).Where(s => !s.IsSliced);

        // Since we dont care about the z-axis transmuting is fine here
        var ordered = sliceables.OrderBy(x => Vector2.Distance(x.transform.position, this.transform.position));
        var nearest = ordered.FirstOrDefault(); // Default is null
        return nearest;
    }

    public float Slice()
    {
        var points = SliceInner();
        var popUp = Instantiate(PointsPopUpPrefab, this.transform.position, Quaternion.identity);
        popUp.GetComponent<TextPopUp>().SetText(((int)points).ToString());
        return points;
    }

    private float SliceInner()
    {
        var dir = Random.onUnitSphere;
        if (dir.y < 0) dir.y *= -1;
        if (!Blade.Slice(dir)) return 0;

        var nearest = GetNearestSliceable();
        if (nearest == null) return Config.Data.MissPenalty;
        if (!_collider.OverlapPoint(nearest.transform.position)) return Config.Data.MissPenalty;

        var distance = Vector2.Distance(this.transform.position, nearest.transform.position);
        var nDistance = distance / _collider.radius;

        return nearest.Slice(dir) * (1f - nDistance);
    }
}
