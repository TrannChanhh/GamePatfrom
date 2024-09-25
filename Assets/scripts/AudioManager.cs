using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("Background Music")]
    public AudioClip introMusic;
    public AudioClip gameMusic;
    public AudioClip endMusic ;

    [Header("AudioSource")]
    public AudioSource audioSource;
    public AudioSource sfxSource;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayBackgroundMusic(introMusic);
    }

    private void PlayBackgroundMusic(AudioClip introMusic)
    {
        audioSource.clip = introMusic;
        audioSource.loop = false;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnGameStarted()
    {
        PlayBackgroundMusic(gameMusic);
    }
    public void OnGameFinished() 
        {
            PlayBackgroundMusic (endMusic);
        }
}
