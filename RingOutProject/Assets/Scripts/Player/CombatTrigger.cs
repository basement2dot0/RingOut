using System.Collections;
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
    private float lastHit;

    private void Start()
    {
        player = GetComponentInParent<Player>();
        opponentsBlockArea = "BlockArea" + player.Opponent.ID.ToString();
        opponentsBody = "Body" + player.Opponent.ID.ToString();
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
                    player.Opponent.IsHit = true;
            }
            isBlock = false;
        }
    }
    private void HitCounter()
    {
        
        if (player.Opponent.IsHit)
        {
            if (player.Opponent.HitCounter > 3)
            {
                player.IsKnockedBack = true;


            }
            player.Opponent.HitCounter++;
        }
    }
    
}
