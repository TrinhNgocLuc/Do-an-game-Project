using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource currentWordAudio;
    private List<AudioSource> sfxs = new List<AudioSource>();
    private List<AudioSource> bgms = new List<AudioSource>();
    private AudioSource currentBgmPlaying;
    private float currentBgmVolume = 1f;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
        currentWordAudio = GetComponent<AudioSource>();
        Transform sfxHolder = transform.Find("SFX");
        foreach (Transform sfx in sfxHolder)
        {
            sfxs.Add(sfx.GetComponent<AudioSource>());
            sfx.GetComponent<AudioSource>().playOnAwake = false;
        }
        Transform bgmHolder = transform.Find("BGM");
        foreach (Transform bgm in bgmHolder)
        {
            bgms.Add(bgm.GetComponent<AudioSource>());
            bgm.GetComponent<AudioSource>().playOnAwake = false;
            bgm.GetComponent<AudioSource>().loop = true;
        }
    }
    public void SetCurrentWordAudio(AudioClip _audio) 
    {
        this.currentWordAudio.clip = _audio;
    }
    public void PlayCurrentWordAudio() => currentWordAudio.Play();
    public void PlaySfx(int index) => sfxs[index].Play();
    public void StopSfx(int index) => sfxs[index].Stop();
    public void PlayBgm(int index)
    {
        if (currentBgmPlaying == bgms[index])
            return;
        StopAllBgm();
        bgms[index].Play();
        currentBgmPlaying = bgms[index];
        currentBgmVolume = currentBgmPlaying.volume;
    }
    public void StopAllBgm()
    {
        foreach (AudioSource bgm in bgms)
        {
            bgm.Stop();
            currentBgmPlaying = null;
        }
    }
    public void DecreaseBGMVolumeWhilePausedGame()
    {
        if (currentBgmPlaying == null)
            return;
        currentBgmPlaying.volume = currentBgmVolume/4f;
    }
    public void IncreaseBGMVolumeAfterContinueGame()
    {
        if (currentBgmPlaying == null)
            return;
        currentBgmPlaying.volume = currentBgmVolume;
    }
}
