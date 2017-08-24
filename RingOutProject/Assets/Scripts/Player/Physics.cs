using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Physics : MonoBehaviour
{
    private Rigidbody rb; 
    private Player player;
    private InputManager inputManager;
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
    private float delay = 0.5f;
    private WaitForSeconds wait;
    private Vector3 defaultPosition;
    private float defaultSpeed = 20.0f;
        

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Initialize(speed, fallMultipler);
        inputManager = GetComponent<InputManager>();
        player = GetComponent<Player>();
        wait = new WaitForSeconds(delay);
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
        BounceBack();
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
    public void KnockedBack()
    {
        if (player.IsKnockedBack)
        {
            player.CanMove = false;
            float yRotation = 90.0f;
            player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, player.transform.eulerAngles.y, yRotation);
            player.transform.position += (player.Opponent.HitDirection) * 20.0f * Time.deltaTime;
            StartCoroutine("GetUp");
        }
    }
    private void UpdatePositon()
    {
        if (CanMove() && player.IsGrounded)
        {
            if (!player.IsDefending && player.IsWalking)
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
            rb.velocity += (Vector3.up + inputManager.Movement(player.ID)) * jumpHeight;
        
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
    
    private void BounceBack()
    {
        if(!player.IsGrounded && player.IsHit)
        {
            
            Vector3 position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            //player.Opponent.transform.position += (player.Opponent.transform.forward * knockBackDistance) * Time.time;
            
            player.transform.position += Vector3.Lerp(position, -(player.transform.forward) * knockBackDistance, Time.deltaTime);
        }
        
    }
    private void PushBack()
    {
        if(player.IsPushed)
        player.Opponent.transform.position += inputManager.Movement(player.Opponent.ID);
    }

    private IEnumerator GetUp()
    {
        yield return wait;
        player.IsKnockedBack = false;
        player.transform.eulerAngles = defaultPosition;
    }
    private bool CanMove()
    {

        if ((Time.time - lastAttack) >= moveDelay)
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

