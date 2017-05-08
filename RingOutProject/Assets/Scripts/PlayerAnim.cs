using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour {

   private Animator anim;
   
    private void Start()
    {
        anim = GetComponent<Animator>();
        
    }
    public void FreeFallAnimation(bool truefalse)
    {
        anim.SetBool("isGrounded", truefalse);
    }
    public void WalkAnimation(bool truefalse)
    {
        anim.SetBool("isWalking", truefalse);
    }

    /// <summary>
    /// Takes an animation trigger or Set or Reset
    /// </summary>
    /// <param name="trigger"></param>
    public void JumpAnimation(AnimationTrigger trigger)
    {
        if (trigger == AnimationTrigger.set)
            anim.SetTrigger("Jump");
        else if (trigger == AnimationTrigger.reset)
            anim.ResetTrigger("Jump");
    }
    public void BlockAnimation(bool truefalse)
    {
        anim.SetBool("isBlocking", truefalse);
    }
}
public enum AnimationTrigger
{
    set,
    reset
}

