using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;

    public void setmusicVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume)*10);
    }
}
