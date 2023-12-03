using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class Blade : MonoBehaviour
{
    public float Radius;
    public AnimationCurve Animation;

    private TrailRenderer _sliceTrail;
    private bool _slicing;

    private Vector3 _start;
    private Vector3 _end;
    private float _startTime;

    private void Awake()
    {
        _sliceTrail = GetComponent<TrailRenderer>();
    }

    /// <summary>
    /// Play the slicing animation
    /// </summary>
    /// <param name="dir">Normalized direction of the slicing animation</param>
    /// <returns>False if not available</returns>
    public bool Slice(Vector2 dir)
    {
        if (!Config.Data.AlwaysAllowSlicing && _slicing) return false;
        _start = dir * Radius;
        _end = dir * Radius * -1;
        _startTime = Time.unscaledTime;
        transform.localPosition = _start;
        _sliceTrail.enabled = true;
        _sliceTrail.Clear();
        _slicing = true;
        return true;
    }

    private void Update()
    {
        if (!_slicing) return;
        var delta = Animation.Evaluate(Time.unscaledTime - _startTime);
        if (delta >= 1)
        {
            StopSlice();
            return;
        }
        this.transform.localPosition = Vector3.Lerp(_start, _end, delta);
    }

    private void StopSlice()
    {
        _sliceTrail.enabled = false;
        _slicing = false;
    }
}
