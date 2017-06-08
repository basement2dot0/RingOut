using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioClip hypeMusic;

    [SerializeField]
    private AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();

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
