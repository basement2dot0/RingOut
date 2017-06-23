using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerAnim))]
[RequireComponent(typeof(Combo))]
[RequireComponent(typeof(Damage))]
[RequireComponent(typeof(AudioManager))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(PlayerAnim))]
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

    //Public Properties 
    public int ID { get { return id; } }
    public bool IsGrounded { get { return isGrounded; } }
    public State CurrentState { get { return currentState; } set { currentState = value; } }

    public bool IsHit { get; internal set; }
    [SerializeField]
    private bool isDefending;
    public bool IsDefending { get { return isDefending; } internal set { isDefending = value; } }

    [SerializeField]
    public Hitbox opponent;
    

    //Unity Methods
    private void Awake()
    {
        foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
        {
            if(item.GetComponent<Player>() != this)
                opponent = item.GetComponent<Hitbox>();
        }
       // Player opponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
        {
            isGrounded = true;
            anim.Jump(AnimationTrigger.reset);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = false;
    }
}