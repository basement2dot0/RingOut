using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOne : MonoBehaviour {

    [SerializeField]
    private float speed;
    private Rigidbody rb;
    private State currentState = State.Idle;
    [SerializeField]
    private float jumpDistance;
    [SerializeField]
    private float rotateSpeed;
    private bool isGrounded;
    private PlayerAnim anim;


    private void Start()
    {
        speed = 10.0f;
        jumpDistance = 20.0f;
        rotateSpeed = 2.0f;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<PlayerAnim>();
        
       
    }

    private void Update()
    {
        Move();
        Block();
    }

    private void FixedUpdate()
    {
        Jump();
    }

    private void Move()
    {
        if (InputManager.Instance.Movement() != Vector3.zero)
        {

            rb.rotation = Quaternion.LookRotation(InputManager.Instance.Movement() * rotateSpeed * Time.deltaTime);
            if (currentState != State.Defending)
            {
                currentState = State.Walking;
                rb.position += InputManager.Instance.Movement() * speed * Time.deltaTime;
                anim.WalkAnimation(true);

            }
        }
        else
            anim.WalkAnimation(false);
    }

    private void Jump()
    {
        if (InputManager.Instance.Movement() != Vector3.zero && InputManager.Instance.GrabButtonDown() && isGrounded)
        {
            currentState = State.Jumping;
            anim.JumpAnimation(AnimationTrigger.set);
            rb.velocity += (Vector3.up * jumpDistance) + InputManager.Instance.Movement() * speed;
        }
        else if (InputManager.Instance.GrabButtonDown() && isGrounded)
        {
            currentState = State.Jumping;
            anim.JumpAnimation(AnimationTrigger.set);
            rb.velocity += Vector3.up * jumpDistance;
        }
        if (!isGrounded)
           rb.velocity += Vector3.down;
        
    }

    private void Block()
    {
        if (InputManager.Instance.DefendButtonDown() && isGrounded)
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
