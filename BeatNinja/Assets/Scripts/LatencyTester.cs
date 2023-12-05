using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LatencyTester : MonoBehaviour
{
    public AudioSource Audio;
    public float BPM;
    public float LatencyOffset;
    public int MeasureInterval;

    public TextMeshProUGUI LatencyText;

    private float _lastBeatPlayed;
    private float _beatDuration;

    private Queue<float> _delays = new();

    void Start()
    {
    }


    void Update()
    {
        _beatDuration = 60.000f / BPM;

        // Play the next beat
        if (Time.unscaledTime - _lastBeatPlayed > _beatDuration)
        {
            Audio.Play();
            _lastBeatPlayed = Time.unscaledTime;
        }
    }

    public void Tap()
    {
        _delays.Enqueue(Time.unscaledTime - _lastBeatPlayed);
        while (_delays.Count > MeasureInterval) _delays.Dequeue();
        UpdateLatency();
    }

    private void UpdateLatency()
    {
        LatencyText.text = (int)(_delays.Average()*1000) + " ms";
    }
}
