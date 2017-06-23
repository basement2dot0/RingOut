using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnim : MonoBehaviour {

    private Animator anim;
    private Player player;
   
    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();
        
    }
    public void IsFalling(bool truefalse)
    {
        anim.SetBool("isGrounded", truefalse);
    }
    public void IsWalking(bool truefalse)
    {
        
        anim.SetBool("isWalking", truefalse);
        
    }
    private IEnumerator CloseAttack()
    {
        yield return null;
        anim.SetBool("isAttacking",false);
    }
    private IEnumerator CloseHypeAttack()
    {
        yield return null;
        anim.SetBool("isHypeAttack", false);
    }
    public void Jump(AnimationTrigger trigger)
    {
        if (trigger == AnimationTrigger.set)
        {
            
            anim.SetTrigger("Jump");
        }
        else if (trigger == AnimationTrigger.reset)
            anim.ResetTrigger("Jump");
    }
    public void IsBlocking(bool truefalse)
    {
        anim.SetBool("isBlocking", truefalse);
       
    }
    public void IsAttacking()
    {
        anim.SetBool("isAttacking", true);
        if (anim.GetBool("isAttacking"))
        {
            
            StartCoroutine("CloseAttack");
        }
       

    }
   
    public void AttackIsHyped(bool truefalse)
    {
       anim.SetBool("isHypeAttack", truefalse);
        if (anim.GetBool("isHypeAttack"))
        {
            StartCoroutine("CloseHypeAttack");
        }
    }

    public void Hit(AnimationTrigger trigger)
    {
        if (trigger == AnimationTrigger.set)
        {

            anim.SetTrigger("isHit");
        }
        else if (trigger == AnimationTrigger.reset)
            anim.ResetTrigger("isHit");
    }
    public void BlockHit(AnimationTrigger trigger)
    {
        if (trigger == AnimationTrigger.set)
        {

            anim.SetTrigger("isBlockHit");
        }
        else if (trigger == AnimationTrigger.reset)
            anim.ResetTrigger("isBlockHit");
    }
    public void IsHypeHit(bool truefalse)
    {
        anim.SetBool("HypeHit", truefalse);
    }
}
public enum AnimationTrigger
{
    set,
    reset
}

