
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class controllVideo : MonoBehaviour
{
    public videoData _videoData;
    [SerializeField] private float seconds;
    private int currentIndex = 0;
    private int countTitle;
    public TextMeshProUGUI titleVideo;
    private bool _pause = false;
    private bool _start = false;
    public GameObject selectVideo;
    public GameObject watchVideo;
    public GameObject pause_button;
    public GameObject play_button;
    public VideoPlayer video;
    public Slider volumeVideo;
    List<VideoClip> _videoClips;
    [SerializeField] private AudioSource SfxButton;

    void Start()
    {
        Get();
        watchVideo.SetActive(false);
        play_button.SetActive(false);
       video.SetDirectAudioVolume(0,volumeVideo.value);
    }

    void Get()
    {
        _videoClips = _videoData.GetData();
        countTitle = currentIndex + 1;
        titleVideo.text = "UNIT " + countTitle.ToString();
        video.clip = _videoClips[currentIndex];

    }
    public void StartVideo()
    {
        SfxButton.Play();
        _start = !_start;
        if (_start)
        {
            selectVideo.SetActive(false);
            watchVideo.SetActive(true);
            video.Play();
        }
        else 
        {
            selectVideo.SetActive(true);
            watchVideo.SetActive(false);
            video.Stop();
        }
    }
    public void pauseVideo() 
    {
        SfxButton.Play();
        _pause = !_pause;
        if (_pause)
        {
            video.Pause();
            pause_button.SetActive(false);
            play_button.SetActive(true);
        }
        else 
        {
            video.Play();
            pause_button.SetActive(true);
            play_button.SetActive(false);
            
        }
    }
    public void stopVideo() 
    {
        SfxButton.Play();
        video.Stop();
        video.Play();

    }
    public void FastForward()
    {
      
        SfxButton.Play();
        video.time += seconds;
    }

    public void Rewind()
    {
        SfxButton.Play();
        video.time -= seconds;
    }
    public void swapLeft()
    {
        SfxButton.Play();
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = 0;
            countTitle = currentIndex + 1;
            titleVideo.text = "UNIT " + countTitle.ToString();
            video.clip = _videoClips[currentIndex];


        }
        else
        {
            countTitle = currentIndex + 1;
            titleVideo.text = "UNIT " + countTitle.ToString();
            video.clip = _videoClips[currentIndex];

        }
    }
    public void swapRight()
    {
        SfxButton.Play();
        currentIndex++;
        if (currentIndex > 5)
        {
            currentIndex = 5;
            countTitle = currentIndex + 1;
            titleVideo.text = "UNIT " + countTitle.ToString();
            video.clip = _videoClips[currentIndex];
        }
        else
        {
            countTitle = currentIndex + 1;
            titleVideo.text = "UNIT " + countTitle.ToString();
            video.clip = _videoClips[currentIndex];
        }
    }
    public void UpdateVolume()
    {
        video.SetDirectAudioVolume(0,volumeVideo.value);
    }
}
