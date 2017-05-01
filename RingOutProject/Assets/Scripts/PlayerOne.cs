using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOne : MonoBehaviour
{

    [SerializeField]
    private float speed;
    private Rigidbody rb;
    private State currentState = State.Idle;
    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private bool isGrounded;
    private PlayerAnim anim;
    private Jump jump;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<PlayerAnim>();
        jump = GetComponent<Jump>();
    }

    private void Update()
    {
        //Debug.Log(rb.velocity.ToString());
       
        Block();
        Move();

    }

    private void FixedUpdate()
    {
       // Jump();
       if(isGrounded)
        jump.JumpInput(rb);
        
    }

    private void Move()
    {

        if (InputManager.Instance.Movement() != Vector3.zero)
        {

            rb.rotation = Quaternion.LookRotation(InputManager.Instance.Movement() * rotateSpeed * Time.deltaTime);
            if (currentState != State.Defending)
            {
                currentState = State.Walking;
                rb.position += InputManager.Instance.Movement() * speed * Time.deltaTime ;
                //transform.Translate(InputManager.Instance.Movement() * speed * Time.deltaTime);
                anim.WalkAnimation(true);

            }
        }
        else
        {

            anim.WalkAnimation(false);
            //if (rb.velocity.x != 0.0f || rb.velocity.z != 0.0f)
            //{
            //    rb.AddForce(Mathf.Clamp(-(rb.velocity.x),-speed,speed),0, Mathf.Clamp(-(rb.velocity.z), -speed, speed));
            //}
        }
    }

    private void Jump()
    {
         
        




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
