using TMPro;
using UnityEngine;

public class TextPopUp : MonoBehaviour
{
    public AnimationCurve Alpha;
    public AnimationCurve Speed;
    public AnimationCurve Rotation;
    public float Randomness;
    public TextMeshPro Text;
    public float Lifetime;

    private float _initTime;
    private float _rng;

    // Start is called before the first frame update
    void Start()
    {
        _initTime = Time.time;
        _rng = Random.value * Randomness;
        Destroy(this.gameObject, Lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        var t = Time.time - _initTime;
        Text.alpha = Alpha.Evaluate(t);

        var rot = Rotation.Evaluate(t + _rng);
        Text.transform.localRotation = Quaternion.EulerRotation(0, 0, rot);

        this.transform.localPosition += new Vector3(0, Speed.Evaluate(t), 0);
    }

    public void SetText(string text)
    {
        Text.text = text;
    }
}
