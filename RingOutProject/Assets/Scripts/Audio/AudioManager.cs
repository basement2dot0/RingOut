using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioClip hypeMusic;
    public AudioClip hit;
    public AudioClip[] attackAudio;


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
    [SerializeField]
    private AudioClip defenseHit;
    [SerializeField]
    private AudioClip airAttackAudio;
    [SerializeField]
    private AudioClip playerHypeHit;
    [SerializeField]
    private AudioClip hypeAttackAudio;

    private void Awake()
    {
        volume = 1.0f;
        player = GetComponent<Player>();
    }
    private void Update()
    {
        playerHit();
        PlayerAttack();
        HypeHit();
        HypeAttack();
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
        if(player.IsHit && player.IsDefending)
        {
            audio.clip = defenseHit;
            audio.Play();
        }
        else if (player.IsHit)
        {
            audio.clip = hit;
            audio.Play();
        }
    }
    private void HypeHit()
    {
        if (player.IsHypeHit && !audio.isPlaying)
        {
            audio.clip = playerHypeHit;
            audio.Play();
        }
    }
    private void PlayerAttack()
    {
        
            if (player.IsGrounded && !player.IsDefending && player.IsAttacking)
            {
                Random rng = new Random();
                audio.clip = attackAudio[player.AttackCounter];
                audio.Play();
            }
            else if(!player.IsGrounded && player.IsAttacking)
            {
            audio.clip = airAttackAudio;
            audio.Play();
            }
        
        
        
        
        
    }
    private void HypeAttack()
    {
        if (player.HypeAttack)
        {
            audio.clip = hypeAttackAudio;
            audio.Play();
        }
    }
}
