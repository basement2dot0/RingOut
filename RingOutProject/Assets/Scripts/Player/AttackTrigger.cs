using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : TriggerManager
{
    
    private static float lastHit;
    [SerializeField]
    private float maxHitCounter;
    [SerializeField]
    private BoxCollider attackCollider;
    private void Awake()
    {
        InitializeMaxCounter(maxHitCounter);
        attackCollider = GetComponent<BoxCollider>();
    }
    protected override void ActivateTriggers(Collider hitbox)
    {
        if (hitbox.name == opponentDefenseHitbox || hitbox.name == opponentsHitbox)
        {
            player.HitDirection = player.transform.forward;
            Debug.Log("Hit Direction:" + player.HitDirection.ToString());
            if (!player.Opponent.IsDefending || isBackAttack())
            {
                if (player.AttackCounter == 3)
                {
                    player.Opponent.IsKnockedBack = true;
                }
            }
            player.Opponent.IsHit = true;
            attackCollider.enabled = false;
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
