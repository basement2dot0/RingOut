using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MomentumBar : MonoBehaviour
{
  
    private Slider momentumBar;
    public bool IsPlayerOne { get; set; }
    [SerializeField]
    private bool isHyped; 
    private Damage damage;
    private AudioManager[] playersTheme;
    private void Awake()
    {
        momentumBar = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<Damage>();
        playersTheme = new AudioManager[2]; 
        foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
        {
            if(item.GetComponent<Player>().ID == 1)
            playersTheme[0] =item.GetComponent<AudioManager>();
            else
                playersTheme[1] = item.GetComponent<AudioManager>();

        }
    }
    private void Update()
    {
        IsMaxed();
    }

    private void IsMaxed()
    {
        
        if (momentumBar.value == momentumBar.maxValue && !isHyped)
        {
            playersTheme[0].PlayHypeMusic();
            isHyped = true;
        }
        if (momentumBar.value == momentumBar.minValue && !isHyped)
        {
            playersTheme[1].PlayHypeMusic();
            isHyped = true;
        }
    }

    public void OnHit()
    {
        
        if (IsPlayerOne)
        {
            if ((momentumBar.value + damage.CurrentDamage(damage.MinDamage, damage.MaxDamage)) > momentumBar.maxValue)
            {
                momentumBar.value = momentumBar.maxValue;
            }
            else
            {
                momentumBar.value += damage.CurrentDamage(damage.MinDamage, damage.MaxDamage);
                
            }
        }
        else
        {
            if ((momentumBar.value - damage.CurrentDamage(damage.MinDamage, damage.MaxDamage)) < momentumBar.minValue)
            {
                momentumBar.value = momentumBar.minValue;
            }
            else
            {
                momentumBar.value -= damage.CurrentDamage(damage.MinDamage, damage.MaxDamage);
            }
        }
        
    }
}
