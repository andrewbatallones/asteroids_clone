using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager bgm;

    public Slider volumeSlider;

    private AudioSource music;


    void Awake()
    {
        music = GetComponent<AudioSource>();

        if (bgm == null)
        {
            bgm = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (volumeSlider != null)
        {
            volumeSlider.maxValue = music.volume;
        }
    }


    public void SetVolume(float newVolume)
    {
        music.volume = newVolume;
    }
}
