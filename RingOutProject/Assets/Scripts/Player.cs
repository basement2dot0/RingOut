using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerAnim))]

public class Player : MonoBehaviour
{
    //Momentum Bar
    private MomentumBar momentumBar;
    
    //Universal Player variables
    [SerializeField]
    public State currentState = State.Idle;
    private Rigidbody rb;
    private PlayerAnim anim;
    private bool isGrounded;
    public bool isHyped;
    [SerializeField]
    private int id;
    private InputManager inputManager;

    public int ID { get { return id; } }

    //Movement Controls
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float jumpDistance;
    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private float jumpDelay;

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
    private float lastJump;


    //Unity Methods
    private void Awake()
    {
        speed = 20.0f;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<PlayerAnim>();
        //lastInput = (inputDelay + 1.0f);
        //lastJump = (jumpDelay);
        inputManager = GetComponent<InputManager>();
        momentumBar = GameObject.FindGameObjectWithTag("Canvas").GetComponent<MomentumBar>();


    }
    private void Start()
    {
        id = inputManager.controlNo;
    }
    private void Update()
    {
        anim.FreeFallAnimation(isGrounded);
        //Attack();
        Block();
        Move();
        //OpenPunchHitBox();



    }
    private void FixedUpdate()
    {

        //Jump();

    }

    private void OnEnable()
    {
        EventManager.Instance.DamageHandler += DamageTaken;
    }

    public void DamageTaken()
    {
        OpenPunchHitBox();
    }

    private void OnDisable()
    {
        EventManager.Instance.DamageHandler -= DamageTaken;
    }

    //Movement Logic
    private void Move()
    {
        if (isGrounded)
        {
            anim.JumpAnimation(AnimationTrigger.reset);

            if (inputManager.Movement(id) != Vector3.zero)
            {

                rb.rotation = Quaternion.LookRotation(inputManager.Movement(id));
                if (currentState != State.Defending)
                {
                    transform.position += inputManager.Movement(id) * speed * Time.deltaTime;
                    currentState = State.Walking;
                    anim.WalkAnimation(true);
                }
            }

            if (inputManager.Movement(id) == Vector3.zero && currentState != State.Defending)
            {
                anim.WalkAnimation(false);
                currentState = State.Idle;
            }
            rb.velocity = Vector3.zero; //remove any velocity applied to player when grounded to prevent unwanted sliding
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
    public void OpenPunchHitBox()
    {
        BoxCollider hitBox = GameObject.FindGameObjectWithTag("Hitbox").GetComponent<BoxCollider>();
        hitBox.enabled = true;
        if(id ==1)
        {
            momentumBar.IsPlayerOne = true;
        }
        else
        {
            momentumBar.IsPlayerOne = false;
            Debug.Log("Player one = false!");
        }
        PunchHitBox.enabled = true;
        
      
    }
    private void ClosePunchHitBox()
    {
        BoxCollider hitBox = GameObject.FindGameObjectWithTag("Hitbox").GetComponent<BoxCollider>();
        hitBox.enabled = false;
    }
    private void OpenKickHitBox()
    {
        KickHitBox.enabled = true;
        rb.velocity += (Vector3.down + transform.forward) * kickVelocity;
    }
    private void CloseKickHitBox() { KickHitBox.enabled = false; }
    private void Block()
    {
        if (isGrounded)
        {
            if (inputManager.DefendButtonDown(id))
            {
                currentState = State.Defending;
                anim.BlockAnimation(true);
            }
            else if (inputManager.DefendButtonUp(id))
            {
                currentState = State.Idle;
                anim.BlockAnimation(false);
            }
        }
    }

}
//    private bool CanJumpForward()
//    {
//        if ((Time.time - lastJump) >= jumpDelay && InputManager.Instance.GrabButtonDown() && InputManager.Instance.Movement() != Vector3.zero)
//            return true;
//        else
//            return false;
//    }
//    private bool CanJump()
//    {
//        if ((Time.time - lastJump) >= jumpDelay && InputManager.Instance.GrabButtonDown())
//            return true;
//        else
//            return false;
//    }
//    private void Jump()
//    {
//        if(isGrounded && currentState != State.Defending)
//        {

//            if (CanJumpForward() )
//            {
//                lastJump = Time.time;
//                currentState = State.Jumping;
//                anim.JumpAnimation(AnimationTrigger.set);
//                rb.velocity += new Vector3(0,jumpHeight, 0) + (jumpDistance * InputManager.Instance.Movement());
//            }
//            else if (CanJump() )
//            {
//                lastJump = Time.time;
//                currentState = State.Jumping;
//                anim.JumpAnimation(AnimationTrigger.set);
//                rb.velocity += Vector3.up * jumpHeight;
//            }
//        }
//        else
//            rb.velocity += Vector3.down * 150 * Time.deltaTime;




//    }


//    //Combat Logic
//    private void OpenPunchHitBox()
//    {
//        PunchHitBox.enabled = true;
//        rb.velocity += (Vector3.down + transform.forward) * punchVelocity;
//    }
//    private void ClosePunchHitBox(){PunchHitBox.enabled = false;}
//    private void OpenKickHitBox()
//    {
//        KickHitBox.enabled = true;
//        rb.velocity += (Vector3.down + transform.forward )* kickVelocity;
//    }
//    private void CloseKickHitBox() { KickHitBox.enabled = false; }
//    private void Attack()
//    {
//       // float sinceLastInput = (Time.time - lastInput);
//        if (isGrounded)
//        {
//            if (InputManager.Instance.AttackButtonDown() && (Time.time - lastInput) >= inputDelay)
//            {
//                if (currentState != State.Defending)
//                {
//                    lastInput = Time.time;
//                    currentState = State.Attacking;
//                    anim.AttackAnimation(true);

//                }
//            }
//            else
//                anim.AttackAnimation(false);

//        }
//        else
//        {
//            if (InputManager.Instance.AttackButtonDown())
//            {
//                currentState = State.Attacking;
//                anim.AttackAnimation(true);
//                lastInput = Time.time;
//            }
//            else
//                anim.AttackAnimation(false);
//        }

//    }

//    public void Hit(Vector3 opponentDirection)
//    {
//        if (currentState != State.Defending)
//        {
//            currentState = State.Hit;
//            rb.AddForce(opponentDirection * 100.0f);
//            anim.HitAnimation(true);
//        }


//    }


