using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collideWithOpponent : MonoBehaviour
{
    private MomentumBar momentumBar;
    private Player player;
    private void Awake()
    {
        player = GetComponentInParent<Player>();
        momentumBar = GameObject.FindGameObjectWithTag("Canvas").GetComponent<MomentumBar>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "BlockArea")
        {
            //negate momentum bar here
            //play block hit animation here
            player.opponent.GetComponent<PlayerAnim>().Hit(AnimationTrigger.set);
        }
        else if(other.name == player.opponent.name)
        {
            if (player.isHyped)
            {
                player.opponent.DamageTaken(player.transform.forward);
                player.opponent.GetComponent<PlayerAnim>().IsHypeHit(true);
            }
            else
            {
                momentumBar.OnHit();
                player.opponent.GetComponent<PlayerAnim>().Hit(AnimationTrigger.set);
            }

        }
    }
}
