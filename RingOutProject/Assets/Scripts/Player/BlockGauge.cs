using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockGauge : MonoBehaviour
{
    private InputManager inputManager;
    private Player player;
    public float blockGauge;
    private Slider gaugeSlider;
    private Text jammerText;
    private bool canBlock;
    

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        player = GetComponent<Player>();
        gaugeSlider = GameObject.FindGameObjectWithTag("gaugeSlider"+player.ID).GetComponent<Slider>();
        gaugeSlider.value = gaugeSlider.maxValue;
        canBlock = true;
        jammerText = gaugeSlider.GetComponentInChildren<Text>();
        jammerText.enabled = false;
    }
    void Update()
    {
        Block();
        BlockGaugeSlider();
        GaugeRecharge();
    }

    private void Block()
    {
        if (player.IsGrounded && canBlock && !player.IsAttacking && !player.IsTaunting && !player.IsExhausted && !player.IsKnockedBack)
        {
            if (Time.timeScale != 0.0f && inputManager.DefendButton(player.ID))
                player.IsDefending = true;
            else if (!inputManager.DefendButton(player.ID))
                player.IsDefending = false;
        }
    }

    private void BlockGaugeSlider()
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
                jammerText.enabled = false;
            }
        }
            
    }

    private void GaugeRecharge()
    {
        if(gaugeSlider.value <= gaugeSlider.minValue)
        {
            canBlock = false;
            jammerText.enabled = true;
            if (player.IsDefending)
            {
                player.IsDefending = false;
            }
            gaugeSlider.value = Mathf.MoveTowards(gaugeSlider.value, gaugeSlider.maxValue, Time.time);
            

        }
    }


}
