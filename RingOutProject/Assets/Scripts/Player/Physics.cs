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
    protected Vector3 direction;
    [SerializeField]
    protected float fallMultipler;
    [SerializeField]
    protected float jumpHeight;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float knockBackDistance; //This may be better off as a Vector3
    [SerializeField]
    protected float lastAttack;
    [SerializeField]
    protected float moveDelay;
    protected float getUpDelay = 1.5f;
    protected WaitForSeconds wait;
    protected Vector3 defaultPosition;
    protected float defaultSpeed = 20.0f;
    


    protected void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Initialize(speed, fallMultipler);
        inputManager = GetComponent<InputManager>();
        player = GetComponent<Player>();
        wait = new WaitForSeconds(getUpDelay);
        defaultPosition = player.transform.eulerAngles;
    }
    
    protected void LateUpdate()
    {
        Jump();
        Gravity();
        AttackMovementRestriction();
        KnockedBack();
        UpdatePositon();
        UpdateRotation();
       RingOut();

    }

    protected void AttackMovementRestriction()
    {
        if (player.IsGrounded)
        {
            if (!player.IsDefending && player.IsAttacking)
                lastAttack = Time.time;
        }
    }
   
    protected void UpdatePositon()
    {
        if (CanMove() && player.IsGrounded || !player.IsKnockedBack)
        {
            if (!player.IsDefending && player.IsWalking)
                transform.position += inputManager.Movement(player.ID) * speed * Time.deltaTime;
        }
        
        
    }
    protected void UpdateRotation()
    {
        if (CanMove() && inputManager.Movement(player.ID) != Vector3.zero)
            rb.rotation = Quaternion.LookRotation(inputManager.Movement(player.ID));
        
    }
    protected void Jump()
    {
        if (player.IsJumping && !player.IsKnockedBack)
            rb.velocity += (Vector3.up * jumpHeight) +(inputManager.Movement(player.ID)*speed);
        
    }
    protected void Gravity()
    {
        if (rb.velocity.y < 0)
            rb.velocity += (-inputManager.Movement(player.ID) + Vector3.up) * UnityEngine.Physics.gravity.y * (fallMultipler - 1) * Time.deltaTime;
    }
    protected void RingOut()
    {
        if (player.IsHypeHit)
            rb.velocity += player.Opponent.transform.forward * 30 * Time.time;
       
    }
    
    protected void KnockedBack()
    {
        if(player.IsKnockedBack)
           StartCoroutine("GetUp");
    }
    protected void Push()
    {
        if(player.IsPushed)
            player.Opponent.transform.position += inputManager.Movement(player.Opponent.ID);
    }
    

    protected IEnumerator GetUp()
    {
        
        float knockBackForce = 10.0f;
        player.transform.forward = -player.Opponent.HitDirection;
        rb.position += player.Opponent.HitDirection  *knockBackForce * Time.deltaTime;
        yield return wait;
        player.IsKnockedBack = false;
        player.CanMove = true;
      //  player.transform.eulerAngles = defaultPosition;
    }
    protected bool CanMove()
    {

        if ((Time.time - player.LastSuccessfulAttack) >= moveDelay && !player.IsKnockedBack && !player.IsExhausted && !player.IsHit)
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
    protected void Initialize(float _speed,float _fallMultipler)
    {
        if (_speed <= 0)
            _speed = defaultSpeed;
        if (_fallMultipler <= 0.0f)
            _fallMultipler = 2.5f;
        speed = _speed;
        fallMultipler = _fallMultipler;
    }
}

