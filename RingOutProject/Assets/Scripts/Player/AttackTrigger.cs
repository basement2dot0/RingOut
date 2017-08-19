using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : TriggerManager
{
    
    private static float lastHit;
    [SerializeField]
    private float maxHitCounter;
    private void Awake()
    {
        InitializeMaxCounter(maxHitCounter);
    }

    private void Update()
    {
        ResetHitCounter();
    }
 

    private void ResetHitCounter()
    {
        if ((Time.time - lastHit) >= 2.5f)
            player.AttackCounter = 0;
    }

    protected override void ActivateTriggers(Collider hitbox)
    {
        if (hitbox.name == opponentDefenseHitbox)
            player.Opponent.IsHit = true;
        else if (hitbox.name == opponentsHitbox)
        {
            if (!player.Opponent.IsDefending || isBackAttack())
            {
                if (player.AttackCounter > maxHitCounter)
                {
                    player.HitDirection = player.transform.forward;
                    player.Opponent.IsKnockedBack = true;
                    player.AttackCounter = 0;
                }
                else
                {

                    player.Opponent.IsHit = true;
                    player.AttackCounter++;
                    lastHit = Time.time;
                }

            }
        }
    }

    /// <summary>
    /// Need to implement logic for checking if the opponent back is towards us at moment of attack registering here
    /// </summary>
    /// <returns></returns>
    private bool isBackAttack()
    {
        Debug.Log(Vector3.Dot(player.transform.position, player.Opponent.transform.position).ToString());
        //if (Vector3.Dot(player.transform.position, player.Opponent.transform.position) != 35.0f)
        if (!player.Opponent.IsDefending)
            return true;
        else
            return false;
        
    }
    private void InitializeMaxCounter(float _maxHitCounter)
    {
        if (_maxHitCounter == 0 )
            _maxHitCounter = 3;
        maxHitCounter = _maxHitCounter;
    }

}
