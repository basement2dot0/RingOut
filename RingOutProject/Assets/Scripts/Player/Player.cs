using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerAnim))]
[RequireComponent(typeof(Combat))]
[RequireComponent(typeof(DamageType))]
[RequireComponent(typeof(AudioManager))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(AnimationManager))]
public class Player : MonoBehaviour{
    
    //Universal Player variables
    [SerializeField]
    private State currentState = State.IDLE;

    [SerializeField]
    private int id;

    [SerializeField]
    private PlayerAnim anim;

    [SerializeField]
    private bool isGrounded;

    [SerializeField]
    public bool isHyped;

    private WaitForSeconds delay;
    [SerializeField]
    private bool canMove;

    //Public Properties 
    public int ID { get { return id; } }
    public bool IsGrounded { get { return isGrounded; } }
    public State CurrentState { get { return currentState; } set { currentState = value; } }

    public bool IsHit { get; internal set; }
    [SerializeField]
    private bool isDefending;
    public bool IsDefending { get { return isDefending; } internal set { isDefending = value; } }

    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }

    public bool IsWalking { get; internal set; }
    public bool IsHypeHit { get; internal set; }
    [SerializeField]
    public bool IsJumping;

    [SerializeField]
    public Hitbox opponent;
    public Player otherPlayer;
    internal bool isAttacking;


    //Unity Methods
    private void Awake()
    {
        canMove = true;
        foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
        {

            if (item.GetComponent<Player>() != this)
            {
                opponent = item.GetComponent<Hitbox>();
                otherPlayer = item.GetComponent<Player>();
            }
        }
        //Player opponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        anim = GetComponent<PlayerAnim>();
        id = GetComponent<InputManager>().ControlNo;
    }
    private void Update()
    {
        anim.IsFalling(isGrounded);
    }
    
    //Grounded Check
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