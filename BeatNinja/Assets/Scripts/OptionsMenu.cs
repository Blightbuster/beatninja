using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    public TMP_InputField LatencyInput;

    public Slider VolumeSlider;

    Resolution[] resolutions;

    void Start()
    {
        VolumeSlider.value = Config.Data.User.Volume;
        LatencyInput.text = (Config.Data.User.LatencyOffset * 1000f).ToString();
        AudioListener.volume = Config.Data.User.Volume;
    }

    public void SetVolume()
    {
        var volume = VolumeSlider.value;
        Config.Data.User.Volume = volume;
        AudioListener.volume = Config.Data.User.Volume;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetLatency()
    {
        var latency = int.Parse(LatencyInput.text) / 1000f;
        Config.Data.User.LatencyOffset = latency;
        Debug.Log($"Set latency to: {latency}");
        SaveSettings();
    }

    public void IncreaseLatency()
    {
        var latency = (int.Parse(LatencyInput.text) + 50) / 1000f;
        Config.Data.User.LatencyOffset = latency;
        Debug.Log($"Set latency to: {latency}");
        LatencyInput.text = (Config.Data.User.LatencyOffset * 1000f).ToString();
        SaveSettings();
    }

    public void DecreaseLatency()
    {
        var latency = (int.Parse(LatencyInput.text) - 50) / 1000f;
        Config.Data.User.LatencyOffset = latency;
        Debug.Log($"Set latency to: {latency}");
        LatencyInput.text = (Config.Data.User.LatencyOffset * 1000f).ToString();
        SaveSettings();
    }

    public void OpenLatencyTest()
    {
        SceneManager.LoadScene("BeatNinjaLatencyTest");
    }

    public void SaveSettings()
    {
        Config.SaveConfig();
    }
}
