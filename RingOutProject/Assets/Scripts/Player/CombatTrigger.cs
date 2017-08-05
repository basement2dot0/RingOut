﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTrigger : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private bool isBlock;
    [SerializeField]
    private string opponentsBlockArea;
    [SerializeField]
    private string opponentsBody;
    private static float lastHit;

    private void Start()
    {
        player = GetComponentInParent<Player>();
        opponentsBlockArea = "BlockArea" + player.Opponent.ID.ToString();
        opponentsBody = "Body" + player.Opponent.ID.ToString();
    }
    private void Update()
    {
        ResetHitCounter();
    }

    private void OnTriggerEnter(Collider other)
    {

        ActivateTriggers(other);
        
    }

    private void ActivateTriggers(Collider hitbox)
    {
        if (hitbox.name == opponentsBlockArea)
        {
            isBlock = true;
            player.Opponent.IsHit = true;
        }
        else if (hitbox.name == opponentsBody)
        {
            if (!isBlock)
            {
                if (player.IsHyped)
                    player.Opponent.IsHypeHit = true;
                else
                {

                    if (player.Opponent.HitCounter > 3)
                    {
                        player.HitDirection = player.transform.forward;
                        player.Opponent.IsKnockedBack = true;
                        player.Opponent.HitCounter = 0;
                    }
                    else
                    {

                        player.Opponent.IsHit = true;
                        player.Opponent.HitCounter++;
                        lastHit = Time.time;
                    }
                }
            }
            isBlock = false;
        }
    }

    private void ResetHitCounter()
    {
        if ((Time.time - lastHit) >= 2.5f)
            player.Opponent.HitCounter = 0;
    }
    
    
    
}
