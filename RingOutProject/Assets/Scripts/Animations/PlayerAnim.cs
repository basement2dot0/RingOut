using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnim : MonoBehaviour {

    private Animator anim;
   
    private void Start()
    {
        anim = GetComponent<Animator>();
        
    }
    public void IsFalling(bool truefalse)
    {
        anim.SetBool("isGrounded", truefalse);
    }
    public void IsWalking(bool truefalse)
    {
        anim.SetBool("isWalking", truefalse);
    }
    public void Jump(AnimationTrigger trigger)
    {
        
        if (trigger == AnimationTrigger.set)
            anim.SetTrigger("Jump");
        else if (trigger == AnimationTrigger.reset)
            anim.ResetTrigger("Jump");
    }
    public void IsBlocking(bool truefalse)
    {
        anim.SetBool("isBlocking", truefalse);
    }
    public void IsAttacking(bool truefalse)
    {
        anim.SetBool("isAttacking", truefalse);
        
    }
    public void AttackIsHyped(bool truefalse)
    {
        anim.SetBool("isAttacking", truefalse);
    }
    public void IsHit(bool truefalse)
    {
        anim.SetBool("isHit", truefalse);
    }
}
public enum AnimationTrigger
{
    set,
    reset
}

