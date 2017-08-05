using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Physics : MonoBehaviour
{
    private Rigidbody rb; 
    private Player player;
    private InputManager inputManager;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float knockBackDistance; //This may be better off as a Vector3
    [SerializeField]
    private float lastAttack;
    [SerializeField]
    private float moveDelay;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (speed < 0)
            speed = 20.0f;
        inputManager = GetComponent<InputManager>();
        player = GetComponent<Player>();
    }
    private void Update()
    {
        if (player.IsAttacking)
        {
            lastAttack = Time.time;
            speed = 0.0f;
        }
    }
    private void LateUpdate()
    {
        Gravity();
        RingOut();
        UpdatePositon();
        UpdateRotation();
        Jump();
        BounceBack();
        KnockedBack();
    }

    public void KnockedBack()
    {
        //need to add conditional if statement somewhere else for when to trigger this knockback
        //player.CanMove = false;
        if (player.IsKnockedBack)
            transform.position += (player.Opponent.transform.forward * knockBackDistance) * Time.time;
        //once we are knocked down, we can then start a coroutine for when we are allowed to get back up ie. isKnockedDown = false

    }
    private void UpdatePositon()
    {
        if (CanMove() && player.IsGrounded)
        {
            if (player.IsWalking)
                transform.position += inputManager.Movement(player.ID) * speed * Time.deltaTime;
        }
        
        
    }
    private void UpdateRotation()
    {
        if (inputManager.Movement(player.ID) != Vector3.zero)
            rb.rotation = Quaternion.LookRotation(inputManager.Movement(player.ID));
    }
    private void Jump()
    {
        if (player.IsJumping)
            rb.velocity += Vector3.up * jumpHeight;
        
    }
    private void Gravity()
    {
        if (!player.IsGrounded)
            rb.velocity += (inputManager.Movement(player.ID) + Vector3.down) * gravity * Time.deltaTime;
    }
    private void RingOut()
    {
        if (player.IsHypeHit)
            rb.velocity += player.Opponent.transform.forward * 30 * Time.time;
       
    }
    private bool CanMove()
    {
        
        if ((Time.time - lastAttack) >= moveDelay)
        {
            speed = 20.0f ;
            return true;
        }
        else
        {
            speed = 0.0f;
            return false;
        }
            
    }
    private void BounceBack()
    {
        if(!player.IsGrounded && player.Opponent.IsHit)
        {
            
            Vector3 position = player.transform.position;
            player.transform.position = Vector3.Lerp(position, -player.transform.forward * knockBackDistance, Time.deltaTime);
        }
        
    }
}

