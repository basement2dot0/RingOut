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
       Jump();
     //  if(isGrounded)
       // jump.JumpInput(rb);
        
    }

    private void Move()
    {
        if (isGrounded)
        {
            if (InputManager.Instance.Movement() != Vector3.zero)
            {

                rb.rotation = Quaternion.LookRotation(InputManager.Instance.Movement() * rotateSpeed * Time.deltaTime);
                if (currentState != State.Defending)
                {
                    anim.WalkAnimation(true);
                    transform.position += (InputManager.Instance.Movement() * speed) * Time.deltaTime;
                }
            }

            else
            {
                anim.WalkAnimation(false);
            }
        }
    }

    private void Jump()
    {
        if(isGrounded)
        {
            if (InputManager.Instance.GrabButtonDown())
            {
                currentState = State.Jumping;
                rb.AddForce(Vector3.up * jumpHeight);
                //transform.position += (Vector3.up * jumpHeight) * Time.deltaTime;
            }
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
