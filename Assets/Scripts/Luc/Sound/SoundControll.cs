using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControll : MonoBehaviour
{
    public AudioSource SoundBackground;
    public Slider SettingVolume;
    void Start()
    {
        SoundBackground.Play();
        SettingVolume.value = SoundBackground.volume;
    }

    public void ChangeVolume()
    {
        float volume = SettingVolume.value;
        SoundBackground.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }
}
