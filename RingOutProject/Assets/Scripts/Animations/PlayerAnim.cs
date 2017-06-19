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

