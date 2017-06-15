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
      if(other.name == "Player2")
        {
            
            Debug.Log(other.name);
            momentumBar.OnHit();
            if (player.isHyped)
            {
                player.opponent.DamageTaken(player.transform.forward);
                player.opponent.GetComponent<PlayerAnim>().IsHit(true);
            }
            else
            {
                //insert normal hit reaction animation here here
            }

        }
    }
}
