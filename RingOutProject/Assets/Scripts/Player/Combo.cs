using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    private InputManager inputManager;
    private PlayerAnim anim;
    private Player player;
    
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        anim = GetComponent<PlayerAnim>();
        player = GetComponent<Player>();

    }
    void Update ()
    {
        Attack();
        Block();
        
    }
    
    private void Attack()
    {
        if (player.isHyped && inputManager.AttackButtonDown(player.ID))
        {
            Debug.Log("HYPE ATTACK!");
            anim.AttackIsHyped(true);
            player.CurrentState = State.ATTACKING;
        }
        if (inputManager.AttackButtonDown(player.ID) )
        {
            Debug.Log("Attacking");
            //anim.IsAttacking(true);
            player.CurrentState = State.ATTACKING;
            //StartCoroutine("CloseAttack");
        }
        else if (!inputManager.AttackButtonDown(player.ID))
        {
            player.CurrentState = State.IDLE;
        }
    }
    private void Block()
    {
        if (player.IsGrounded)
        {
            if (inputManager.DefendButton(player.ID))
                player.CurrentState = State.DEFENDING;
            else if (!inputManager.DefendButton(player.ID))
                player.CurrentState = State.IDLE;
        }
    }
}
