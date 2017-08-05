using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private InputManager inputManager;
    private Player player;
    [SerializeField]
    private float resetAttack;
    private WaitForSeconds delay;

    private float lastAttack;
    [SerializeField]
    private float attackDelay;
    private void Awake()
    {
        delay = new WaitForSeconds(resetAttack);
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

        if (!player.IsHyped)
        {
            if(CanAttack() && inputManager.AttackButtonDown(player.ID))
            {
                lastAttack = Time.time;
                player.IsAttacking = true;
                // StartCoroutine("ResetAttack");
            }
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
            {
                player.HypeAttack = true;
                StartCoroutine("ResetHype");
            }
            else if (player.HypeAttack && !player.IsHyped)
                player.HypeAttack = false;
        }
    }
    private bool CanAttack()
    {
        if ((Time.time - lastAttack) >= attackDelay)
        {
            player.IsAttacking = false;
            return true;
        }
        else
            return false;
    }
    private IEnumerator ResetAttack()
    {
        yield return null;
        player.IsAttacking = false;
    }
    private IEnumerator ResetHype()
    {
        yield return delay;
        player.IsHyped = false;
    }
    
}
