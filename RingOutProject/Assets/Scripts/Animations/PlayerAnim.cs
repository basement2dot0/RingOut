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
        if (truefalse)
            player.CurrentState = State.WALKING;
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
            player.CurrentState = State.JUMPING;
            anim.SetTrigger("Jump");
        }
        else if (trigger == AnimationTrigger.reset)
            anim.ResetTrigger("Jump");
    }
    public void IsBlocking(bool truefalse)
    {
        if(truefalse)
            player.CurrentState = State.DEFENDING;
        anim.SetBool("isBlocking", truefalse);
    }
    public void IsAttacking(bool truefalse)
    {
        anim.SetBool("isAttacking", truefalse);
        if (truefalse)
        {
            player.CurrentState = State.ATTACKING;
            StartCoroutine("CloseAttack");
        }
       

    }
    public void IsIdle(bool truefalse)
    {
        if (truefalse)
            player.CurrentState = State.IDLE;
        anim.SetBool("isIdle", truefalse);
    }
    public void AttackIsHyped(bool truefalse)
    {
        if (truefalse)
            player.CurrentState = State.ATTACKING;

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

