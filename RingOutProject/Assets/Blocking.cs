using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocking : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == player.opponent.name)
        {
            //if()
        }
    }
}
