using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip hypeMusic;
    
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
       
    }
    private void Start()
    {
        
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
    public void AttackFX()
    {

    }

    public void SpecialFX() { }
    public void JumpFX() { }
    


}
