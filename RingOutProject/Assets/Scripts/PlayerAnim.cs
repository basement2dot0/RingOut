using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour {

   private Animator anim;
   
    private void Start()
    {
        anim = GetComponent<Animator>();
        
    }
    public void WalkAnimation(bool truefalse)
    {
        anim.SetBool("isWalking", truefalse);
    }
}
