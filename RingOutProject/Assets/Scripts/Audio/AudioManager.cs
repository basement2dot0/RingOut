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

    private float defaultVolume;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        audio.volume = 1.0f;
    }

    public void PlayHypeMusic()
    {
        audio.clip = hypeMusic;
        audio.PlayOneShot(audio.clip);
    }
    public void AttackFX()
    {

    }
    public void FadeHypeMusic(float fadeSource)
    {
        audio.volume = fadeSource;

        if(audio.volume < 0.01f)
        {
            audio.Stop();
            audio.volume = 1.0f;
        }
    }
    public void SpecialFX() { }
    public void JumpFX() { }



}
