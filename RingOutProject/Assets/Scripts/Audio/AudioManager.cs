using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioClip hypeMusic;


    //[SerializeField]
    //private float fadeDuration;

    [SerializeField]
    private AudioSource audio;
    [SerializeField]
    private float volume
    {
        
        get
        {
            if (audio == null)
             audio = GetComponent<AudioSource>(); 
           return audio.volume;
           
        }
        set
        {
            if(audio == null)
                audio = GetComponent<AudioSource>();
            audio.volume = value;
        }
    }

    private float defaultVolume;

    private void Awake()
    {
        volume = 1.0f;
    }
    public void PlayHypeMusic()
    {
        audio.clip = hypeMusic;
        audio.PlayOneShot(audio.clip);
    }
    public void StopHypeMusic()
    {
        audio.Stop();
    }
    public void FadeHypeMusic(float fadeSource)
    {
        volume = fadeSource;
        if(volume < 0.01f)
        {
            audio.Stop();
            volume = 1.0f;
        }
    }
}
