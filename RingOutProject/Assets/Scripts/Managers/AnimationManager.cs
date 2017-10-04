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
    [SerializeField]
    private WaitForSeconds hypeDelay;
    public bool canAttack
    {
        get
            {
            return CanAttack();
            }
        
     }

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();
        inputManager = GetComponent<InputManager>();
        if(player.name == string.Format("Xiao"))
            hypeDelay = new WaitForSeconds(.5f);
        else
            hypeDelay = new WaitForSeconds(1.5f);
        attackDelay = 1.0f;

    }
    private void Update()
    {
        ResetAttackCounter();
        HypeTaunt();
        HypeAttack();
        Attack();
        IsFalling();
        Walk();
        Jump();
        Block();
        HypeHit();
        Exhausted();
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
        //anim.SetBool("isBlocking", player.IsDefending);
        if (player.IsGrounded)
        { 
            if (player.CanBlock && inputManager.DefendButton(player.ID) && (Time.time - player.LastSuccessfulAttack) > 1.0f && !player.IsTaunting && !player.IsExhausted && !player.IsKnockedBack)
            {
                anim.Play("Block");
                player.IsDefending = true;
            }
            else if (!inputManager.DefendButton(player.ID))
            {
                player.IsDefending = false;
            }
        }
        
    }
    private void Attack()
    {
        
            if (!player.IsDefending && CanAttack() && inputManager.AttackButtonDown(player.ID))
            {
                 player.CanBlock = false;
            if (!player.IsHyped)
            {
                player.IsAttacking = true;
                AttackManager();
                
            }
        }
        
    }
    private void IsKnockedBack()
    {
        if (player.IsKnockedBack)
           anim.Play("KnockBack");
        
    }
    private void HypeTaunt()
    {
        if (player.IsTaunting)
        {

            
            StartCoroutine("ResetTaunt");
        }
    }
    private void HypeAttack()
    {
        if (player.IsHyped && !player.IsTaunting && !player.IsDefending)
        {
            
            if ((inputManager.AttackButtonDown(player.ID) && player.IsGrounded && !player.IsKnockedBack))
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
        // anim.SetBool("hypeHit", player.IsHypeHit);
        if (player.IsHypeHit)
        {
            anim.Play("HypeHit");
            StartCoroutine("ResetHypeHit");
        }
    }
    private void Hit()
    {
        if (player.IsHit)
        {
            anim.Play("Hit");
        }
    }
    private void Exhausted()
    {
        if (player.IsExhausted)
        {
            anim.Play("Exhausted");
            StartCoroutine("ExhaustReset");
        }
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
                player.AttackCounter = 0;
                player.LastSuccessfulAttack = Time.time;
            }
            player.IsAttacking = true;
        }
        else
        {
            anim.Play("JumpAttack");
            player.LastSuccessfulAttack = Time.time;
            player.IsAttacking = true;
        }

        }
    private bool CanAttack()
    {

        if ((Time.time - player.LastSuccessfulAttack) >= attackDelay && Time.timeScale != 0.0f && !player.IsKnockedBack && !player.Opponent.IsTaunting)
            return true;
        else
        {
            player.IsAttacking = false;
            return false;

        }
   }
    private IEnumerator ResetJump(bool value)
    {
        yield return jumpDelay;
        value = false;
    }
    //private IEnumerator ResetHit( )
    //{
    //    yield return null;
    //    player.IsHit = false;
    //}
    private IEnumerator ExhaustReset()
    {
        WaitForEndOfFrame exhaustWait = new WaitForEndOfFrame();
        WaitForSeconds waitForSeconds = new WaitForSeconds(2.0f);
        yield return waitForSeconds;
        player.IsExhausted = false;
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
        player.IsExhausted = true;

        
    }
    private IEnumerator ResetTaunt()
    {
        player.Opponent.gameObject.active = false;
        anim.Play("HypeTaunt");
        WaitForSeconds delay = new WaitForSeconds(6.0f);
        yield return delay;
        player.IsTaunting = false;
        player.Opponent.gameObject.active = true;
        
    }
    private IEnumerator ResetHypeHit()
    {
        WaitForSeconds wait = new WaitForSeconds(2.0f);
        yield return wait;
        player.IsHypeHit = false;
    }



}