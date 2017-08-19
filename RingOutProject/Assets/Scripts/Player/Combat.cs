using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private InputManager inputManager;
    private Player player;
    [SerializeField]
    private float resetAttack;
    [SerializeField]
    private WaitForSeconds delay;
    [SerializeField]
    private WaitForSeconds hypeDelay;

    private float lastAttack;
    [SerializeField]
    private float attackDelay;
    private float lastAttackCounter;
    private float lastSuccessfulAttack;

    private void Awake()
    {
        delay = new WaitForSeconds(resetAttack);
        hypeDelay = new WaitForSeconds(1.5f);
        inputManager = GetComponent<InputManager>();
        player = GetComponent<Player>();
    }
    void Update()
    {
        Debug.Log(CanAttack().ToString());
        HypeAttack();
        Attack();
        ResetHitCounter();
        Block();
    }
    
    private void Attack()
    {

        if (!player.IsHyped && ResetHitCounter() <= 3)
        {

            if ((inputManager.AttackButtonDown(player.ID) && !player.IsGrounded))
            {
                player.IsAttacking = true;
                StartCoroutine("ResetAttack");
            }
            else if (CanAttack() && inputManager.AttackButtonDown(player.ID))
            {
                lastAttack = Time.time;
                player.IsAttacking = true;
                player.AttackCounter++;
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
        {
            // player.IsAttacking = false;
            return true;
        }
        else
        {

            return false;
        }
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
    private float ResetHitCounter()
    {
        if ((Time.time - lastSuccessfulAttack) >= 1.0f)
            player.AttackCounter = 0;
        return player.AttackCounter;
    }

}
