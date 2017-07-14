using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioClip hypeMusic;

    [SerializeField]
    private float fadeDuration;

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
    public void AttackFX()
    {

    }
    public void FadeHypeMusic()
    {
        audio.volume = Mathf.MoveTowards(audio.volume, 0.0f, Time.deltaTime * fadeDuration);
        if(audio.volume < 0.1f)
        {
            audio.Stop();
            audio.volume = 1;
        }
    }
    public void SpecialFX() { }
    public void JumpFX() { }



}
