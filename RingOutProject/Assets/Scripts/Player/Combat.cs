using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    /// <summary>
    /// I want to add a jump attack. I was going to add a new method called jump attack and call it in update by doing a if(player.Airborne): attack ; jumpAttack
    /// however this would go against the DRY principle.
    /// I believe the characters animator controller has a check for grounded, if it does then I will simply use the same logic
    /// and simply add an "isAttacking" flow control from the jump animation state to the jump attack aniamtion state, this means that there wont be any
    /// need for additional code.
    /// if However i dont have a airbrone bool in the character controller and it is not a simply  task to add one
    /// Then I will call this jump attack directly from inside the PlayerAnimator.cs componenet
    /// </summary>
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
    private bool hasAttacked;
    
    private void Attack()
    {
        if (player.isHyped && inputManager.AttackButtonDown(player.ID))
            anim.AttackIsHyped(true);
        else if (inputManager.AttackButtonDown(player.ID))
            anim.IsAttacking();
    }

    private void Block()
    {
        if (player.IsGrounded)
        {
            if (inputManager.DefendButton(player.ID))
            {
                player.CanMove = false;
                anim.IsBlocking(true);
            }
            else if (!inputManager.DefendButton(player.ID))
            {
                anim.IsBlocking(false);
                player.CanMove = true;
            }
        }
    }

   
}
