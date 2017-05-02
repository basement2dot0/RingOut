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
    private State currentState = State.Idle;
    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private bool isGrounded;
    private PlayerAnim anim;
   // private Jump jump;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<PlayerAnim>();
       // jump = GetComponent<Jump>();
    }

    private void Update()
    {
        //Debug.Log(rb.velocity.ToString());
       
        Block();
        Move();

    }

    private void FixedUpdate()
    {
        Debug.Log(transform.position.y.ToString());
        Jump();
     //  if(isGrounded)
       // jump.JumpInput(rb);
        
    }

    private void Move()
    {
        if (isGrounded)
        {
            anim.JumpAnimation(AnimationTrigger.reset);
            if (InputManager.Instance.Movement() != Vector3.zero)
            {

                rb.rotation = Quaternion.LookRotation(InputManager.Instance.Movement() * rotateSpeed * Time.deltaTime);
                if (currentState != State.Defending)
                {
                    currentState = State.Walking;
                    anim.WalkAnimation(true);
                    transform.position += (InputManager.Instance.Movement() * speed) * Time.deltaTime;
                }
            }

            else
            {
                anim.WalkAnimation(false);
                currentState = State.Idle;
                rb.velocity += new Vector3(-(rb.velocity.x), 0, -(rb.velocity.z));
            }
        }
    }

    private void Jump()
    {
        if(isGrounded)
        {
            if (InputManager.Instance.GrabButtonDown() && InputManager.Instance.Movement() != Vector3.zero)
            {
                currentState = State.Jumping;
                anim.JumpAnimation(AnimationTrigger.set);
                rb.velocity += new Vector3(0,jumpHeight, 0) + (jumpDistance * transform.forward);
                //transform.position += (Vector3.up * jumpHeight) * Time.deltaTime;
            }
            else if (InputManager.Instance.GrabButtonDown())
            {
                rb.velocity += Vector3.up * jumpHeight;
            }
        }
        else
        {
            rb.velocity += Vector3.down;
        }

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
