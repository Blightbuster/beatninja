using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class SliceArea : MonoBehaviour
{
    public Blade Blade;
    public AudioClip SliceSound;
    public GameObject PointsPopUpPrefab;
    public GameObject MaxPointsPopUpPrefab;

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
        _spriteRenderer.color = Color.red * nearest.distance;
    }

    private (Sliceable? sliceable, float distance) GetSliceableInArea()
    {
        var cam = Camera.main.transform;
        Debug.DrawRay(cam.position, _collider.transform.position - cam.position, Color.red);
        var ray = new Ray(cam.position, (_collider.transform.position - cam.position).normalized);
        //if(!Physics.Raycast(ray, out var hitInfo, 200, 1 << LayerMask.NameToLayer("Sliceable"))) return null;
        if (!Physics.SphereCast(ray, _collider.radius, out var hitInfo, 200, 1 << LayerMask.NameToLayer("Sliceable"))) return (null, 0);
        Debug.DrawLine(Camera.main.transform.position, hitInfo.point, Color.green, 0.5f);
        return (hitInfo.transform.GetComponent<Sliceable>(), GetNormedDistanceToArea(hitInfo.point));
    }

    public float GetDistanceToArea(Vector3 point)
    {
        var cam = Camera.main.transform;
        var ray = new Ray(cam.position, (_collider.transform.position - cam.position).normalized);
        return Vector3.Cross(ray.direction, point - ray.origin).magnitude;
    }

    public float GetNormedDistanceToArea(Vector3 point)
    { 
        return Mathf.Max(0, (GetDistanceToArea(point) / _collider.radius) - 0.2f);
    }

    public float Slice()
    {
        var points = SliceInner();
        var isMaxHit = points == Config.Data.MaxHitPoints;
        var prefab = isMaxHit ? MaxPointsPopUpPrefab : PointsPopUpPrefab;
        var popUp = Instantiate(prefab, this.transform.position, Quaternion.identity);
        if (!isMaxHit) popUp.GetComponentInChildren<TextPopUp>().SetText(PointsToText(points));
        if (points > 0) AudioSource.PlayClipAtPoint(SliceSound, Camera.main.transform.position, 0.1f);
        return points;
    }

    public string PointsToText(int points)
    {
        if (points < 0) return "MISS";
        if (points < 30) return "BAD";
        if (points < 50) return "OK";
        if (points < 80) return "GOOD";
        if (points < 90) return "SUPER";
        return "MAX";
    }

    private int SliceInner()
    {
        var dir = Random.onUnitSphere;
        if (dir.y < 0) dir.y *= -1;
        if (!Blade.Slice(dir)) return 0;

        var nearest = GetSliceableInArea();
        if (nearest.sliceable == null) return Config.Data.MissPenalty;
        return (int)(nearest.sliceable.Slice(dir) * (1f - nearest.distance));
    }
}
