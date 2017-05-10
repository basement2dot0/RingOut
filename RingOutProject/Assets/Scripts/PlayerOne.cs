using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOne : MonoBehaviour
{

    [SerializeField]
    private float speed;

    [SerializeField]
    private float jumpHeight;

    [SerializeField]
    private float jumpDistance;

    private Rigidbody rb;

    [SerializeField]
    private State currentState = State.Idle;

    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private bool isGrounded;

    private PlayerAnim anim;

    [SerializeField]
    private BoxCollider PunchHitBox;
    [SerializeField]
    private BoxCollider KickHitBox;



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
   
    private void Move()
    {
        if (isGrounded)
        {
            anim.JumpAnimation(AnimationTrigger.reset);
            
            if (InputManager.Instance.Movement() != Vector3.zero)
            {

                rb.rotation = Quaternion.LookRotation(InputManager.Instance.Movement());
                if (currentState != State.Defending)
                {
                    transform.position += InputManager.Instance.Movement() * speed * Time.deltaTime;
                    currentState = State.Walking;
                    anim.WalkAnimation(true);
                }
            }

            else if(InputManager.Instance.Movement() == Vector3.zero && currentState != State.Defending)
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
           
            if (InputManager.Instance.GrabButtonDown() && InputManager.Instance.Movement() != Vector3.zero)
            {
                currentState = State.Jumping;
                anim.JumpAnimation(AnimationTrigger.set);
                rb.velocity += new Vector3(0,jumpHeight, 0) + (jumpDistance * InputManager.Instance.Movement());
            }
            else if (InputManager.Instance.GrabButtonDown())
            {
                currentState = State.Jumping;
                anim.JumpAnimation(AnimationTrigger.set);
                rb.velocity += Vector3.up * jumpHeight;
            }
        }
        else
        {
            rb.velocity += Vector3.down * 150 * Time.deltaTime;
        }
            
        

    }

    private void Block()
    {
        if (isGrounded)
        {
            if (InputManager.Instance.DefendButtonDown())
            {
                currentState = State.Defending;
                anim.BlockAnimation(true);
            }
            else if (InputManager.Instance.DefendButtonUp())
            {
                currentState = State.Idle;
                anim.BlockAnimation(false);
            }
        }
    }

    private void OpenPunchHitBox()
    {
        PunchHitBox.enabled = true;
    }
    private void ClosePunchHitBox()
    {
        PunchHitBox.enabled = false;
    }
    private void OpenKickHitBox() { KickHitBox.enabled = true; }
    private void CloseKickHitBox() { KickHitBox.enabled = false; }
    private void Attack()
    {
        //May need an inputDelay
        if (InputManager.Instance.AttackButtonDown())
        {
           currentState = State.Attacking;
            if (!isGrounded)
            {
                anim.AttackAnimation(true);
                rb.velocity += (Vector3.down + transform.forward) * 50.0f;
            }
            else
            {
                anim.AttackAnimation(true);
                rb.velocity += transform.forward * 50.0f;
            }
                

        }
        else if (InputManager.Instance.AttackButtonUP())
        {
            anim.AttackAnimation(false);
        }
    }
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

public enum State
{
    Idle,
    Walking,
    Attacking,
    Defending,
    Jumping
};
