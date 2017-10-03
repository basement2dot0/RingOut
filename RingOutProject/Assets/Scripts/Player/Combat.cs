using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat : MonoBehaviour
{
    private InputManager inputManager;
    private Player player;
    public float blockGauge;
    private Slider gaugeSlider;
    private bool canBlock;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        player = GetComponent<Player>();
        gaugeSlider = GameObject.FindGameObjectWithTag("gaugeSlider"+player.ID).GetComponent<Slider>();
        gaugeSlider.value = gaugeSlider.maxValue;
        canBlock = true;
    }
    void Update()
    {
        Block();
        BlockGauge();
        GaugeRecharge();
    }

    private void Block()
    {
        if (player.IsGrounded && canBlock)
        {
            if (Time.timeScale != 0.0f && inputManager.DefendButton(player.ID))
                player.IsDefending = true;
            else if (!inputManager.DefendButton(player.ID))
                player.IsDefending = false;
        }
    }

    private void BlockGauge()
    {
        if (player.IsDefending)
        {
            gaugeSlider.value--;
            if(gaugeSlider.value <= gaugeSlider.minValue)
            {
                gaugeSlider.value = gaugeSlider.minValue;
            }
        }

        else
        {
            gaugeSlider.value++;
            if (gaugeSlider.value >= gaugeSlider.maxValue)
            {
                gaugeSlider.value = gaugeSlider.maxValue;
                canBlock = true;
            }
        }
            
    }

    private void GaugeRecharge()
    {
        if(gaugeSlider.value <= gaugeSlider.minValue)
        {
            canBlock = false;
            if (player.IsDefending)
            {
                player.IsDefending = false;
            }
            gaugeSlider.value = Mathf.MoveTowards(gaugeSlider.value, gaugeSlider.maxValue, Time.time);
            

        }
    }


}
