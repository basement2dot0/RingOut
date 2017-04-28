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
    private bool isGrounded;
    private PlayerAnim anim;


    private void Start()
    {
        speed = 10.0f;
        jumpDistance = 20.0f;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<PlayerAnim>();
        
       
    }

    private void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        Jump();
    }

    private void Move()
    {
        if (InputManager.Instance.Movement() != Vector3.zero)
        {
            rb.rotation = Quaternion.LookRotation(InputManager.Instance.Movement());
            rb.position += InputManager.Instance.Movement() * speed * Time.deltaTime;
            anim.WalkAnimation(true);
        }
        else
            anim.WalkAnimation(false);
    }

    private void Jump()
    {
        if (InputManager.Instance.Movement() != Vector3.zero && InputManager.Instance.GrabButtonDown() && isGrounded)
        {
            Debug.Log("Jump distance activated!");
            rb.velocity += (Vector3.up * jumpDistance) + InputManager.Instance.Movement() * speed;
        }
        else if (InputManager.Instance.GrabButtonDown() && isGrounded)
            rb.velocity += Vector3.up * jumpDistance;
        
        if (!isGrounded)
            rb.velocity += Vector3.down;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = true;
        
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
    Grab
};
