using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioClip hypeMusic;
    public AudioClip hit;
    public AudioClip attacking;


    [SerializeField]
    private Player player;

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
        player = GetComponent<Player>();
    }
    private void Update()
    {
        playerHit();
        PlayerAttack();
    }


    public void PlayHypeMusic()
    {
        if (player.IsHyped)
        {
            audio.clip = hypeMusic;
            audio.Play();
        }
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

    private void playerHit()
    {
        if (player.IsHit)
        {
            audio.clip = hit;
            audio.PlayOneShot(audio.clip);
        }
    }

private void PlayerAttack()
    {
        if (player.IsAttacking)
        {
            audio.clip = attacking;
            audio.Play();
        }
    }
}
