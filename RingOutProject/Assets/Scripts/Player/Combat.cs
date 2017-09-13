using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private InputManager inputManager;
    private Player player;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        player = GetComponent<Player>();
    }
    void Update()
    {
        Block();
    }

    private void Block()
    {
        if (player.IsGrounded)
        {
            if (Time.timeScale != 0.0f && inputManager.DefendButton(player.ID))
                player.IsDefending = true;
            else if (!inputManager.DefendButton(player.ID))
                player.IsDefending = false;
        }
    }


}
