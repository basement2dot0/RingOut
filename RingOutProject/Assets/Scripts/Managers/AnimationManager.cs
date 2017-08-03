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
        Attack();
        Block();
    }
    private void IsFalling()
    {
        anim.SetBool("isGrounded", player.IsGrounded);
        if (!player.IsGrounded && anim.GetBool("isWalking"))
            player.IsWalking = false;
        

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
        anim.SetBool("isAttacking", player.isAttacking);

    }
    private void Jump()
    {
        
            anim.SetBool("IsJumping",player.IsJumping);
        StartCoroutine("ResetBool", player.IsJumping);


    }
    private void Hit()
    {
        if (player.IsHit)
            anim.SetTrigger("IsHit");
        StartCoroutine("ResetBool", player.IsHit);
        
    }

    private IEnumerator ResetBool(bool value)
    {
        
        yield return null;
        value = false;
        
       
    }
}
public enum AnimationTrigger
{
    set,
    reset
}