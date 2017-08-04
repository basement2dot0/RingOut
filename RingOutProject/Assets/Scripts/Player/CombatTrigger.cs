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
    private string blockArea;
    [SerializeField]
    private string body;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        blockArea = "BlockArea" + player.Opponent.ID.ToString();
        body = "Body" + player.Opponent.ID.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        ActivateTriggers(other);
    }

    private void ActivateTriggers(Collider hitbox)
    {
        if (hitbox.name.Equals(blockArea))
            isBlock = true;
        else if (hitbox.name.Equals(body))
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
}
