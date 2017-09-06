using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationManager : MonoBehaviour
{

    private Animator anim;
    private Player player;
    private WaitForSeconds delay = new WaitForSeconds(0.5f);


    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();

    }
    private void Update()
    {
        Attack();
        IsFalling();
        Walk();
        Jump();
        HypeAttack();
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
            if (player.IsAttacking)
                anim.SetTrigger("isAttacking");
            else
                anim.ResetTrigger("isAttacking");
        }
    }
    private void IsKnockedBack()
    {
        anim.SetBool("isKnockedBack", player.IsKnockedBack);
    }
    private void HypeAttack()
    {
        anim.SetBool("hypeAttack", player.HypeAttack);
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
            StartCoroutine("Reset");
        }
        else
            anim.ResetTrigger("isHit");
    }

    private IEnumerator ResetJump(bool value)
    {
        yield return delay;
        value = false;
    }
    private IEnumerator Reset( )
    {
        yield return null;
        player.IsHit = false;
    }
}