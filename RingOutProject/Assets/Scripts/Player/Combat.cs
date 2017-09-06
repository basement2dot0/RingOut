using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{

    [SerializeField]
    private float resetAttack;
    [SerializeField]
    private float attackDelay;
    [SerializeField]
    private WaitForSeconds delay;
    [SerializeField]
    private WaitForSeconds hypeDelay;

    private InputManager inputManager;
    private Player player;
    private float lastAttack;
    
    private void Awake()
    {
        delay = new WaitForSeconds(resetAttack);
        hypeDelay = new WaitForSeconds(1.5f);
        inputManager = GetComponent<InputManager>();
        player = GetComponent<Player>();
    }
    void Update()
    {
        HypeAttack();
        ResetAttackCounter();
        Attack();
        Block();
    }
    
    private void Attack()
    {

        if (!player.IsHyped)
        {

            if ((inputManager.AttackButtonDown(player.ID) && !player.IsGrounded))
            {
                player.IsAttacking = true;
                StartCoroutine("ResetAttack");
            }
            if (CanAttack() && inputManager.AttackButtonDown(player.ID))
            {
                lastAttack = Time.time;
                player.IsAttacking = true;
                StartCoroutine("ResetAttack");
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
        if (CanAttack() && player.IsGrounded)
        {
            if ( player.IsHyped && inputManager.AttackButtonDown(player.ID))
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
            return true;
        else
            return false;
    }
    private float ResetAttackCounter()
    {

        if ((Time.time - player.LastSuccessfulAttack) >= 1.0f)
            player.AttackCounter = 0;
        return player.AttackCounter;
    }
    private IEnumerator ResetAttack()
    {
        yield return delay;
        player.IsAttacking = false;
    }
    private IEnumerator ResetHype()
    {
        yield return hypeDelay;
        player.IsHyped = false;
    }
}
