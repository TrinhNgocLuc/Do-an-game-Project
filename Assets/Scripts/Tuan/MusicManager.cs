using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;

    public AudioClip BgMusic;

    private static MusicManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        musicSource.clip = BgMusic;
        musicSource.Play();
    }
    private void Update()
    {
        CheckSceneForMusic();
    }

    private void CheckSceneForMusic()
    {
        string[] scenesWithMusic = { "Level1", "Level2", "Level3", "Level4", "Level5", "LevelSelect", "StartScene" };

        if (System.Array.Exists(scenesWithMusic, element => element == SceneManager.GetActiveScene().name))
        {
            PlayMusic();
        }
        else
        {
            StopMusic();
        }
    }

    public void PlayMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }
    //public void StopMusic()
    //{
    //    if (musicSource.isPlaying)
    //    {
    //        musicSource.Stop();
    //    }
    //}
}
