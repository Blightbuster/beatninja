using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMPro.TMP_Dropdown resolutionDropdown;

    public TMPro.TMP_Text latencySliderText;

    Resolution[] resolutions;

    void Start() 
    {
        // set resolution dropdown options
        
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for(int i=0; i<resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetVolume(float volume) 
    {
        // since the audioMixer volume is not linear!!!
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20); 
    }

    public void SetFullscreen(bool isFullscreen) 
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetLatency(float latency)
    {
        Config.Data.User.LatencyOffset = latency;
        latencySliderText.text = latency.ToString("0.00");
    }
}