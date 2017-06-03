using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collideWithOpponent : MonoBehaviour
{
    Player player;
    private MomentumBar momentumBar;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        momentumBar = GameObject.FindGameObjectWithTag("Canvas").GetComponent<MomentumBar>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.DamageTaken();
            momentumBar.OnHit();
        }
    }
}
