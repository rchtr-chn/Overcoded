using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsScript : MonoBehaviour
{
    public TMP_Dropdown ResDropdown;
    private Resolution[] _resolutions;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _musicSlider;

    private void Start()
    {
        _resolutions = Screen.resolutions;

        ResDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + "x" + _resolutions[i].height;
            options.Add(option);

            if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        ResDropdown.AddOptions(options);
        ResDropdown.value = currentResolutionIndex;
        ResDropdown.RefreshShownValue();
    }

    public void setFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    
    public void setmusicVolume(float volume)
    {
        _audioMixer.SetFloat("volume", Mathf.Log10(volume)*10);
    }
}


