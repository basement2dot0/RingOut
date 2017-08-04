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
    private Hitbox hitbox;
    private float speed;
    [SerializeField]
    private float knockBackDistance; //This may be better off as a Vector3

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (speed < 0)
            speed = 20.0f;
        inputManager = GetComponent<InputManager>();
        hitbox = GetComponent<Hitbox>();
    }
    private void LateUpdate()
    {
        Gravity();
        UpdatePositon();
        UpdateRotation();
        Jump();
        RingOut();
        KnockedBack();
    }

    public void KnockedBack()
    {
        //need to add conditional if statement somewhere else for when to trigger this knockback
        //player.CanMove = false;
        if (player.IsKnockedBack)
            transform.position += player.Opponent.transform.forward * knockBackDistance* Time.time;
    }
    private void UpdatePositon()
    {
        if(player.IsWalking)
            transform.position += inputManager.Movement(player.ID) * speed * Time.deltaTime;
    }
    private void UpdateRotation()
    {
        if (inputManager.Movement(player.ID) != Vector3.zero)
            rb.rotation = Quaternion.LookRotation(inputManager.Movement(player.ID));
    }
    private void Jump()
    {
        if (player.IsJumping)
        {
            rb.velocity += Vector3.up * jumpHeight;
        }
    }
    private void Gravity()
    {
        if (!player.IsGrounded)
            rb.velocity += (inputManager.Movement(player.ID) + Vector3.down) * gravity * Time.deltaTime;
    }
    private void RingOut()
    {
        if (player.IsHypeHit)
        {
            //Debug.Log(hitDireciton.ToString());
            rb.velocity += player.Opponent.transform.forward * 30 * Time.time;
        }
    }
}

