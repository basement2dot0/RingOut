﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounceBack : MonoBehaviour {

    private Player player;
    private string body;
    [SerializeField]
    float dist;
	// Use this for initialization
	void Start () {
        player = GetComponentInParent<Player>();
        body = "Body" + player.Opponent.ID.ToString();
	}
    private void OnTriggerStay(Collider other)
    {
        if(other.name == "Ground" && !player.IsGrounded)
        {
            float distance = Vector3.Distance(other.transform.position, player.transform.position) + dist;
            Vector3 target = -player.transform.forward;
            player.transform.Translate(target * distance* Time.deltaTime);
        }
    }

}
