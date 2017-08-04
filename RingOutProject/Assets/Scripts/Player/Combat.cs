using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private InputManager inputManager;
    private Player player;
    [SerializeField]
    private float attackDelay;
    private WaitForSeconds delay;
    
    private void Awake()
    {
        delay = new WaitForSeconds(attackDelay);
        inputManager = GetComponent<InputManager>();
        player = GetComponent<Player>();
    }
    void Update()
    {
        HypeAttack();
        Attack();
        Block();
    }
    
    private void Attack()
    {
        
        if (!player.IsHyped && inputManager.AttackButtonDown(player.ID))
        {

            player.IsAttacking = true;
            StartCoroutine("InputDelay");
        }

    }
    private void Block()
    {
        if (player.IsGrounded)
        {
            if (inputManager.DefendButton(player.ID))
                player.IsDefending = true;
            else if (!inputManager.DefendButton(player.ID))
                player.IsDefending = false;
            
        }
    }
    private void HypeAttack()
    {
        if (player.IsGrounded)
        {
            if (player.IsHyped && inputManager.AttackButtonDown(player.ID))
                player.HypeAttack = true;
            else if (player.HypeAttack && !player.IsHyped)
                player.HypeAttack = false;
        }
    }
    
    private IEnumerator InputDelay()
    {
        
        yield return delay;
        player.IsAttacking = false;
        
    }
}
