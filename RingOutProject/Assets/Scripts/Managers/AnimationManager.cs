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
        IsFalling();
        Walk();
        Jump();
        HypeAttack();
        Attack();
        Block();
        HypeHit();
    }
    private void LateUpdate()
    {
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

        anim.SetBool("hypeAttack", player.HypeAttack);
        anim.SetBool("isAttacking", player.IsAttacking);
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