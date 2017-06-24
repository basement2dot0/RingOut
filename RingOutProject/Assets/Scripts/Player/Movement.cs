using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    //Required Components
    private Rigidbody rb;
    private Player player;
    private InputManager inputManager;
    private PlayerAnim playerAnim;
    
    
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
        inputManager = GetComponent<InputManager>();
        speed = 20.0f;
        playerAnim = GetComponent<PlayerAnim>();
        hitbox = GetComponent<Hitbox>();
        
    }
    
    // Update is called once per frame
    void Update ()
    {
            Move();
        RingOut(hitbox.hitDireciton);
    }

    private void RingOut(Vector3 hitDireciton)
    {
        if(player.IsHit)
        {
            Debug.Log(hitDireciton.ToString());
            rb.velocity += hitDireciton * 30 * Time.time;
        }
    }

    private void FixedUpdate()
    {
        Jump();
    }

    //Movement Logic
    private void Move()
    {
        if (player.IsGrounded)
        {
            playerAnim.Jump(AnimationTrigger.reset);
            if (inputManager.Movement(player.ID) != Vector3.zero)
            {
                rb.rotation = Quaternion.LookRotation(inputManager.Movement(player.ID));
                if (player.CurrentState != State.DEFENDING)
                {
                    playerAnim.IsWalking(true);
                    player.CurrentState = State.WALKING;
                    transform.position += inputManager.Movement(player.ID) * speed * Time.deltaTime;
                }
            }
            if (inputManager.Movement(player.ID) == Vector3.zero && player.CurrentState != State.DEFENDING)
            {
                playerAnim.IsWalking(false);
                player.CurrentState = State.IDLE;
            }
            rb.velocity = Vector3.zero; //remove any velocity applied to player when grounded to prevent unwanted sliding
        }
    }
    
    //Jump Logic
    private void Jump()
    {
        if (player.IsGrounded && player.CurrentState != State.DEFENDING)
        {
            if (CanJumpForward())
            {
                lastJump = Time.time;
                player.CurrentState = State.JUMPING;
                playerAnim.Jump(AnimationTrigger.set);
                rb.velocity += new Vector3(0, jumpHeight, 0) + (jumpDistance * inputManager.Movement(player.ID));
            }
            else if (CanJump())
            {
                lastJump = Time.time;
                player.CurrentState = State.JUMPING;
                playerAnim.Jump(AnimationTrigger.set);
                rb.velocity += Vector3.up * jumpHeight;
            }
        }
        else
            rb.velocity +=( inputManager.Movement(player.ID)+Vector3.down) * gravity * Time.deltaTime;
    }
    private bool CanJumpForward()
    {
        if ((Time.time - lastJump) >= jumpDelay && inputManager.JumpButtonDown(player.ID) && inputManager.Movement(player.ID) != Vector3.zero)
            return true;
        else
            return false;
    }
    private bool CanJump()
    {
        if ((Time.time - lastJump) >= jumpDelay && inputManager.JumpButtonDown(player.ID))
            return true;
        else
            return false;
    }
}
