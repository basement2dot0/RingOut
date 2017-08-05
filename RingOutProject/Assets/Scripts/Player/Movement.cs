using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    //Required Components
    private Player player;
    private InputManager inputManager;
    //Jump Controls
    private float lastJump;
    [SerializeField]
    private float jumpDelay;
    
   // Use this for initialization
    void Awake ()
    {
        player = GetComponent<Player>();
        inputManager = GetComponent<InputManager>();
    }
    // Update is called once per frame
    void Update ()
    {
        Move();
        Jump();
    }
    private void Move()
    {
        if (player.IsGrounded && player.CanMove)
        {
            if (inputManager.Movement(player.ID) != Vector3.zero)
                player.IsWalking = true;

            if (inputManager.Movement(player.ID) == Vector3.zero)
                player.IsWalking = false;
            //rb.velocity = Vector3.zero; //remove any velocity applied to player when grounded to prevent unwanted sliding
        }
        else if (!player.IsGrounded && inputManager.Movement(player.ID) != Vector3.zero)
            player.IsWalking = false;
    }
    private void Jump()
    {
        if (player.IsGrounded )
        {
            if (inputManager.JumpButtonDown(player.ID)&& CanJump())
            {
                lastJump = Time.time;
                player.IsJumping = true;
                StartCoroutine("JumpReset");
            }
        }
        //rb.velocity += (inputManager.Movement(player.ID) + Vector3.down) * gravity * Time.deltaTime;
    }

    private bool CanJump()
    {
        if ((Time.time - lastJump) >= jumpDelay)
            return true;
        else
            return false;
    }
    //private bool CanJumpForward()
    //{
    //    if ((Time.time - lastJump) >= jumpDelay && inputManager.JumpButtonDown(player.ID) && inputManager.Movement(player.ID) != Vector3.zero)
    //        return true;
    //    else
    //        return false;
    //}

    private IEnumerator JumpReset()
    {
        yield return null;
        player.IsJumping = false;
    }
}
