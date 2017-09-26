﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MomentumBar : MonoBehaviour
{
    private Player[] players; 
    private Slider momentumBar;
    [SerializeField]
    private bool isHyped; 
    private DamageType damage;
    private AudioManager[] playersTheme;
    [SerializeField]
    private float startingValue;
    [SerializeField]
    private float hypeTimer;
    [SerializeField]
    private Text hypeText;
    [SerializeField]
    private bool isTimer;
    [SerializeField]
    private bool isPlayerOne;

    public bool IsHyped { get {return isHyped; } }
    public bool IsPlayerOne
    {
        get { return isPlayerOne; }
        set { isPlayerOne = value; }
    }


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
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<DamageType>(); // this needs to be dynamically assigned between both players damage type
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
        ActivePlayer();
        UpdateBar();
        IsMaxed();
        ResetMomentumBar();
    }

    private void ActivePlayer()
    {
        if (players[0].IsHit)
            isPlayerOne = false;
        else if (players[1].IsHit)
            isPlayerOne = true;
    }
    private void IsMaxed()
    {
            if (momentumBar.value == momentumBar.maxValue && !isHyped)
            {
                playersTheme[0].PlayHypeMusic();
                hypeText.text = "Player One is HYPED!";
                players[0].IsHyped = true;
                isHyped = true;
                isTimer = true;
            }
            else if (momentumBar.value == momentumBar.minValue && !isHyped)
            {
                playersTheme[1].PlayHypeMusic();
                hypeText.text = "Player Two is HYPED!";
                players[1].IsHyped = true;
                isHyped = true;
                isTimer = true;
            }
    }

    private void PlayerDamage()
    {
        if (isPlayerOne)
        {
            if ((momentumBar.value + players[0].DamageType.CurrentDamage(damage.MinDamage, damage.MaxDamage)) > momentumBar.maxValue)
                momentumBar.value = momentumBar.maxValue;
            else
                momentumBar.value += players[0].DamageType.CurrentDamage(players[0].DamageType.MinDamage, players[0].DamageType.MaxDamage);
        }
        else 
        {
            if ((momentumBar.value + players[1].DamageType.CurrentDamage(damage.MinDamage, damage.MaxDamage)) < momentumBar.minValue)
                momentumBar.value = momentumBar.minValue;
            else
                momentumBar.value -= players[1].DamageType.CurrentDamage(players[1].DamageType.MinDamage, players[1].DamageType.MaxDamage);
        }


    }
    private void PlayerDamage(float negate)
    {
        if (isPlayerOne)
            momentumBar.value += players[0].DamageType.CurrentDamage(damage.MinDamage, damage.MaxDamage)/negate;
        else
            momentumBar.value -= players[1].DamageType.CurrentDamage(damage.MinDamage, damage.MaxDamage)/ negate;
    }

    public void UpdateBar()
    {
        if (players[0].IsDefending && players[0].IsHit || players[1].IsDefending && players[1].IsHit)
        {
            float halfDamage = 2.0f;
            if (!isTimer)
                PlayerDamage(halfDamage);
        }
        else if (players[0].IsHit || players[1].IsHit)
        {
            if (!isTimer)
                PlayerDamage();
        }
        
    }
    public void ResetMomentumBar()
    {
        if (isHyped)
        {

            if (!players[0].IsHyped && !players[1].IsHyped || isHyped && momentumBar.value == startingValue)
            {
                isHyped = false;
                isTimer = false;
                hypeText.text = "";
                momentumBar.value = startingValue;
                players[0].IsHyped = false;
                players[1].IsHyped = false;
                playersTheme[0].StopHypeMusic();
                playersTheme[1].StopHypeMusic();
            }
            else if (players[0].IsHyped)
            {
                momentumBar.value = Mathf.MoveTowards(momentumBar.value, startingValue, Time.deltaTime * hypeTimer);
                playersTheme[0].FadeHypeMusic((momentumBar.value - startingValue) / 50);
                
            }
            else if (players[1].IsHyped)
            {
                momentumBar.value = Mathf.MoveTowards(momentumBar.value, startingValue, Time.deltaTime * hypeTimer);
                playersTheme[1].FadeHypeMusic((momentumBar.value + startingValue) / 50);
            }
        }
    }
    

}
