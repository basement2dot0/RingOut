using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MomentumBar : MonoBehaviour
{
    private Player[] players; 
    private Slider momentumBar;
    public bool IsPlayerOne { get; set; }
    [SerializeField]
    private bool isHyped; 
    private Damage damage;
    private AudioManager[] playersTheme;
    [SerializeField]
    private float startingValue;
    [SerializeField]
    private float HypeTimer;
    [SerializeField]
    private Text hypeText;

    public bool IsHyped { get {return isHyped; } }

    private void Awake()
    {
        players = new Player[2];
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponent<Player>().ID == 1)
                players[0] = player.GetComponent<Player>();
            else
                players[1] = player.GetComponent<Player>();

        }
        momentumBar = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<Damage>();
        startingValue = 50.0f;
        momentumBar.value = startingValue;
        hypeText = gameObject.transform.GetChild(0).GetComponent<Text>();
        playersTheme = new AudioManager[2]; 
        foreach (var theme in GameObject.FindGameObjectsWithTag("Player"))
        {
            if(theme.GetComponent<Player>().ID == 1)
            playersTheme[0] =theme.GetComponent<AudioManager>();
            else
                playersTheme[1] = theme.GetComponent<AudioManager>();

        }
    }
    private void Update()
    {
        IsMaxed();
        if (isHyped)
        {
            ResetMomentumBar();
        }
    }

    private void IsMaxed()
    {
        
        if (momentumBar.value == momentumBar.maxValue && !isHyped)
        {
            playersTheme[0].PlayHypeMusic();
            hypeText.text = "Player One is HYPED!";
            players[0].isHyped = true;
            isHyped = true;
        }
        else if (momentumBar.value == momentumBar.minValue && !isHyped)
        {
            playersTheme[1].PlayHypeMusic();
            hypeText.text = "Player Two is HYPED!";
            players[1].isHyped = true;
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

    public void ResetMomentumBar()
    {
        if(momentumBar.value == startingValue)
        {
            players[0].isHyped = false;
            players[1].isHyped = false;
            isHyped = false;
            hypeText.text = "";
            playersTheme[0].StopHypeMusic();
            playersTheme[1].StopHypeMusic();

        }
        momentumBar.value = Mathf.MoveTowards(momentumBar.value, startingValue, Time.deltaTime * HypeTimer);
        
     }
}
