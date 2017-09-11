using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationManager : MonoBehaviour
{

    private Animator anim;
    private Player player;
    private WaitForSeconds jumpDelay = new WaitForSeconds(0.5f);
    private InputManager inputManager;
    [SerializeField]
    private float attackDelay;
    private WaitForSeconds hypeDelay;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();
        inputManager = GetComponent<InputManager>();
        hypeDelay = new WaitForSeconds(1.5f);
        attackDelay = 1.0f;

    }
    private void Update()
    {
        ResetAttackCounter();
        HypeAttack();
        Attack();
        IsFalling();
        Walk();
        Jump();
        Block();
        HypeHit();
    }
    private void LateUpdate()
    {
        IsKnockedBack();
        Hit();
    }

    private void IsFalling()
    {
        if (!player.IsHypeHit)
        {
            anim.SetBool("isGrounded", player.IsGrounded);
            if (!player.IsGrounded && anim.GetBool("isWalking"))
                player.IsWalking = false;
        }
    }
    private void Walk()
    {
        anim.SetBool("isWalking", player.IsWalking);
    }
    private void Block()
    {
        anim.SetBool("isBlocking", player.IsDefending);
    }
    private void Attack()
    {
        if (!player.IsHyped)
        {
            if (CanAttack() && inputManager.AttackButtonDown(player.ID))
                AttackManager();
        }
        
    }
    private void IsKnockedBack()
    {
        if (player.IsKnockedBack)
           anim.Play("KnockBack");
        
    }
    private void HypeAttack()
    {
        if (player.IsHyped)
        {
            if ((inputManager.AttackButtonDown(player.ID) && player.IsGrounded))
            {
                anim.Play("HypeAttack");
                StartCoroutine("ResetHype");
            }
        }
           
    }
    private void Jump()
    {
        anim.SetBool("isJumping", player.IsJumping);
        StartCoroutine("ResetJump", player.IsJumping);
    }
    private void HypeHit()
    {
        anim.SetBool("hypeHit", player.IsHypeHit);
    }
    private void Hit()
    {
        if (player.IsHit)
        { 
            anim.SetTrigger("isHit");
            StartCoroutine("ResetHit");
        }
        else
            anim.ResetTrigger("isHit");
    }

    private void AttackManager()
    {
        if (player.IsGrounded)
        {
            if (player.AttackCounter == 0)
            {
                anim.Play("Attack");
                player.LastSuccessfulAttack = Time.time;
            }
            else if (player.AttackCounter == 1)
            {
                anim.Play("Attack2");
                player.LastSuccessfulAttack = Time.time;
            }
            else if (player.AttackCounter == 2)
            {
                anim.Play("Attack3");
                player.LastSuccessfulAttack = Time.time;
            }
            else if (player.AttackCounter >= 3)
            {
                anim.Play("Attack");
                player.LastSuccessfulAttack = Time.time;
            }
        }
        else
            anim.Play("JumpAttack");
        
    }
    private bool CanAttack()
    {

        if ((Time.time - player.LastSuccessfulAttack) >= attackDelay)
            return true;
        else
            return false;


    }
    private IEnumerator ResetJump(bool value)
    {
        yield return jumpDelay;
        value = false;
    }
    private IEnumerator ResetHit( )
    {
        yield return null;
        player.IsHit = false;
    }

    private float ResetAttackCounter()
    {

        if ((Time.time - player.LastSuccessfulAttack) >= 1.5f)
            player.AttackCounter = 0;
        return player.AttackCounter;
    }
    private IEnumerator ResetHype()
    {
        yield return hypeDelay;
        player.IsHyped = false;
    }



}