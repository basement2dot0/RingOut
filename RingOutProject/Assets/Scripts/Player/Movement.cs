using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    //Required Components
    private Rigidbody rb;
    private Player player;
    private InputManager inputManager;
    

    //Movement Controls
    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotateSpeed;

    //Jump Controls
    private float lastJump;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float jumpDistance;
    [SerializeField]
    private float jumpDelay;
    
    // Use this for initialization
    void Awake ()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
        inputManager = GetComponent<InputManager>();
        speed = 20.0f;
    }
    
    // Update is called once per frame
    void Update ()
    {
        Move();
	}
    private void FixedUpdate()
    {
        Jump();
    }

    //Movement Logic
    private void Move()
    {
        if (player.IsGrounded)
        {
            if (inputManager.Movement(player.ID) != Vector3.zero)
            {
                rb.rotation = Quaternion.LookRotation(inputManager.Movement(player.ID));
                if (player.CurrentState != State.DEFENDING)
                {
                    transform.position += inputManager.Movement(player.ID) * speed * Time.deltaTime;
                    player.CurrentState = State.WALKING;
                    //anim.IsWalking(true);
                }
            }
            if (inputManager.Movement(player.ID) == Vector3.zero && player.CurrentState != State.DEFENDING)
            {
                //anim.IsWalking(false);
                player.CurrentState = State.IDLE;
            }
            rb.velocity = Vector3.zero; //remove any velocity applied to player when grounded to prevent unwanted sliding
        }
    }
    
    //Jump Logic
    private void Jump()
    {
        if (player.IsGrounded && player.CurrentState != State.DEFENDING)
        {
            if (CanJumpForward())
            {
                lastJump = Time.time;
                player.CurrentState = State.JUMPING;
                rb.velocity += new Vector3(0, jumpHeight, 0) + (jumpDistance * inputManager.Movement(player.ID));
            }
            else if (CanJump())
            {
                lastJump = Time.time;
                player.CurrentState = State.JUMPING;
                rb.velocity += Vector3.up * jumpHeight;
            }
        }
        else
            rb.velocity += Vector3.down * 150 * Time.deltaTime;
    }
    private bool CanJumpForward()
    {
        if ((Time.time - lastJump) >= jumpDelay && inputManager.JumpButtonDown(player.ID) && inputManager.Movement(player.ID) != Vector3.zero)
            return true;
        else
            return false;
    }
    private bool CanJump()
    {
        if ((Time.time - lastJump) >= jumpDelay && inputManager.JumpButtonDown(player.ID))
            return true;
        else
            return false;
    }
}
