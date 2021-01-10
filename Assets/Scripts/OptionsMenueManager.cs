using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenueManager : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider volumeSlider;


    private void Awake()
    {
        SetVolumneSlider();
    }


    public void SetVolume(float newVolume)
    {
        masterMixer.SetFloat("masterVolume", newVolume);
    }


    private void SetVolumneSlider()
    {
        float value;

        masterMixer.GetFloat("masterVolume", out value);
        volumeSlider.value = value;
    }
}
