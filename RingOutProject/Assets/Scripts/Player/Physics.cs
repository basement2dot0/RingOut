using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Physics : MonoBehaviour
{
    protected Rigidbody rb;
    protected Player player;
    protected InputManager inputManager;
    private Vector3 direction;
    [SerializeField]
    private float fallMultipler;
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
    private float getUpDelay = 1.5f;
    private WaitForSeconds wait;
    private Vector3 defaultPosition;
    private float defaultSpeed = 20.0f;
    


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Initialize(speed, fallMultipler);
        inputManager = GetComponent<InputManager>();
        player = GetComponent<Player>();
        wait = new WaitForSeconds(getUpDelay);
        defaultPosition = player.transform.eulerAngles;
    }
    
    private void LateUpdate()
    {
        Jump();
        Gravity();
        AttackMovementRestriction();
        KnockedBack();
        UpdatePositon();
        UpdateRotation();
       RingOut();

    }

    private void AttackMovementRestriction()
    {
        if (player.IsGrounded)
        {
            if (!player.IsDefending && player.IsAttacking)
                lastAttack = Time.time;
        }
    }
   
    private void UpdatePositon()
    {
        if (CanMove() && player.IsGrounded || !player.IsKnockedBack)
        {
            if (!player.IsDefending && player.IsWalking)
                transform.position += inputManager.Movement(player.ID) * speed * Time.deltaTime;
        }
        
        
    }
    private void UpdateRotation()
    {
        if (CanMove() && inputManager.Movement(player.ID) != Vector3.zero)
            rb.rotation = Quaternion.LookRotation(inputManager.Movement(player.ID));
        
    }
    private void Jump()
    {
        if (player.IsJumping && !player.IsKnockedBack)
            rb.velocity += (Vector3.up * jumpHeight) +(inputManager.Movement(player.ID)*speed);
        
    }
    private void Gravity()
    {
        if (rb.velocity.y < 0)
            rb.velocity += (-inputManager.Movement(player.ID) + Vector3.up) * UnityEngine.Physics.gravity.y * (fallMultipler - 1) * Time.deltaTime;
    }
    private void RingOut()
    {
        if (player.IsHypeHit)
            rb.velocity += player.Opponent.transform.forward * 30 * Time.time;
       
    }
    
    private void KnockedBack()
    {
        if(player.IsKnockedBack)
           StartCoroutine("GetUp");
    }
    private void Push()
    {
        if(player.IsPushed)
            player.Opponent.transform.position += inputManager.Movement(player.Opponent.ID);
    }
    

    private IEnumerator GetUp()
    {
        
        float knockBackForce = 10.0f;
        player.transform.forward = -player.Opponent.HitDirection;
        rb.position += player.Opponent.HitDirection  *knockBackForce * Time.deltaTime;
        yield return wait;
        player.IsKnockedBack = false;
        player.CanMove = true;
      //  player.transform.eulerAngles = defaultPosition;
    }
    private bool CanMove()
    {

        if ((Time.time - player.LastSuccessfulAttack) >= moveDelay && !player.IsKnockedBack && !player.IsTaunting)
        {
            speed = defaultSpeed;
            player.CanMove = true;
            return true;
        }
        else
        {
            speed = 0.0f;
            player.CanMove = false;
            return false;
        }

    }
    private void Initialize(float _speed,float _fallMultipler)
    {
        if (_speed <= 0)
            _speed = defaultSpeed;
        if (_fallMultipler <= 0.0f)
            _fallMultipler = 2.5f;
        speed = _speed;
        fallMultipler = _fallMultipler;
    }
}

