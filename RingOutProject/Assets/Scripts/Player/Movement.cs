using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    //Required Components
    private Rigidbody rb;
    private Player player;
    private InputManager inputManager;
    
    
    //Movement Controls
    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private float gravity;

    //Jump Controls
    private float lastJump;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float jumpDistance;
    [SerializeField]
    private float jumpDelay;
    private Hitbox hitbox;
   
    // Use this for initialization
    void Awake ()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
        if(speed < 0)
            speed = 20.0f;
        inputManager = GetComponent<InputManager>();
        hitbox = GetComponent<Hitbox>();
    }
    // Update is called once per frame
    void Update ()
    {
        Move();
        Jump();
        RingOut(hitbox.HitDireciton);
    }
    private void FixedUpdate()
    {
       if (inputManager.Movement(player.ID) != Vector3.zero)
            rb.rotation = Quaternion.LookRotation(inputManager.Movement(player.ID));
    }

    
    private void Move()
    {
        
        if (player.IsGrounded && player.CanMove)
        {
            
            if (inputManager.Movement(player.ID) != Vector3.zero)
            {
                    player.IsWalking = true;
                    transform.position += inputManager.Movement(player.ID) * speed * Time.deltaTime;
                    
            }
            if (inputManager.Movement(player.ID) == Vector3.zero)
            {
                player.IsWalking = false;
            }
            rb.velocity = Vector3.zero; //remove any velocity applied to player when grounded to prevent unwanted sliding
        }
       
    }
    private void Jump()
    {
        if (player.IsGrounded )
        {
            if (inputManager.JumpButtonDown(player.ID)&& CanJump())
            {
                lastJump = Time.time;
                player.IsJumping = true;
                rb.velocity += Vector3.up * jumpHeight;
                StartCoroutine("JumpReset");
            }
        }
        else
            rb.velocity += (inputManager.Movement(player.ID) + Vector3.down) * gravity * Time.deltaTime;
    }
    private void RingOut(Vector3 hitDireciton)
    {
        if (player.IsHypeHit)
        {
            //Debug.Log(hitDireciton.ToString());
            rb.velocity += hitDireciton * 30 * Time.time;
        }
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
