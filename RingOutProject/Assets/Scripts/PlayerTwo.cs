using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerAnim))]
public class PlayerTwo : MonoBehaviour
{
    //Universal Player variables
    [SerializeField]
    public State currentState = State.Idle;
    private Rigidbody rb;
    private PlayerAnim anim;
    private bool isGrounded;
    
    //Movement Controls
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float jumpDistance;
    [SerializeField]
    private float rotateSpeed;
    
    //Combat Controls
    [SerializeField]
    private BoxCollider PunchHitBox;
    [SerializeField]
    private BoxCollider KickHitBox;
    [SerializeField]
    private float punchVelocity;
    [SerializeField]
    private float kickVelocity;
    [SerializeField]
    private float inputDelay;
    private float lastInput;


    //Unity Methods
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<PlayerAnim>();
        
    }
    private void Update()
    {
        anim.FreeFallAnimation(isGrounded);
        Block();
        Move();
        Attack();
    }
    private void FixedUpdate()
    {
       
        Jump();
        
    }

    
    //Movement Logic
    private void Move()
    {
        if (isGrounded)
        {
            anim.JumpAnimation(AnimationTrigger.reset);
            
            if (InputManagerTwo.Instance.Movement() != Vector3.zero)
            {

                rb.rotation = Quaternion.LookRotation(InputManagerTwo.Instance.Movement());
                if (currentState != State.Defending)
                {
                    transform.position += InputManagerTwo.Instance.Movement() * speed * Time.deltaTime;
                    currentState = State.Walking;
                    anim.WalkAnimation(true);
                }
            }

            else if(InputManagerTwo.Instance.Movement() == Vector3.zero && currentState != State.Defending)
            {
                anim.WalkAnimation(false);
                currentState = State.Idle;
            }
            rb.velocity = Vector3.zero; //remove any velocity applied to player when grounded to prevent unwanted sliding
        }
        
    }
    private void Jump()
    {
        if(isGrounded && currentState != State.Defending)
        {
           
            if (InputManagerTwo.Instance.GrabButtonDown() && InputManagerTwo.Instance.Movement() != Vector3.zero)
            {
                currentState = State.Jumping;
                anim.JumpAnimation(AnimationTrigger.set);
                rb.velocity += new Vector3(0,jumpHeight, 0) + (jumpDistance * InputManager.Instance.Movement());
            }
            else if (InputManagerTwo.Instance.GrabButtonDown())
            {
                currentState = State.Jumping;
                anim.JumpAnimation(AnimationTrigger.set);
                rb.velocity += Vector3.up * jumpHeight;
            }
        }
        else
            rb.velocity += Vector3.down * 150 * Time.deltaTime;
        
            
        

    }
    
    
    //Combat Logic
    private void OpenPunchHitBox()
    {
        PunchHitBox.enabled = true;
        rb.velocity += (Vector3.down + transform.forward) * punchVelocity;
    }
    private void ClosePunchHitBox(){PunchHitBox.enabled = false;}
    private void OpenKickHitBox()
    {
        KickHitBox.enabled = true;
        rb.velocity += transform.forward * kickVelocity;
    }
    private void CloseKickHitBox() { KickHitBox.enabled = false; }
    private void Attack()
    {
        if (InputManagerTwo.Instance.AttackButtonDown())
        {
                if ((lastInput - Time.deltaTime) >= inputDelay)
                {
                    currentState = State.Attacking;
                    anim.AttackAnimation(true);
                    lastInput = Time.deltaTime;
                }
        }
        else if (InputManagerTwo.Instance.AttackButtonUP())
            anim.AttackAnimation(false);
        
    }
    private void Block()
    {
        if (isGrounded)
        {
            if (InputManagerTwo.Instance.DefendButtonDown())
            {
                currentState = State.Defending;
                anim.BlockAnimation(true);
            }
            else if (InputManagerTwo.Instance.DefendButtonUp())
            {
                currentState = State.Idle;
                anim.BlockAnimation(false);
            }
        }
    }
    public void Hit(Vector3 opponentDirection)
    {
        if (currentState != State.Defending)
        {
            currentState = State.Hit;
            rb.AddForce(opponentDirection * 100.0f);
            anim.HitAnimation(true);
        }
        
        
    }

    
    //Ground Check
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            anim.JumpAnimation(AnimationTrigger.reset);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = false;

    }
    
}