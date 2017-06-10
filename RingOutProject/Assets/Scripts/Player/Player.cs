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
    
    //Momentum Bar
    private MomentumBar momentumBar;

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

    //Combat Controls
    [SerializeField]
    private BoxCollider PunchHitBox;

    [SerializeField]
    private BoxCollider KickHitBox;

    [SerializeField]
    private float punchVelocity;

    [SerializeField]
    private float kickVelocity;
   
    //Unity Methods
    private void Awake()
    {
       
        anim = GetComponent<PlayerAnim>();
        id = GetComponent<InputManager>().ControlNo;
        momentumBar = GameObject.FindGameObjectWithTag("Canvas").GetComponent<MomentumBar>();
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

    private void CheckState()
    {
        switch (currentState)
        {
            case State.IDLE:
                //anim.IsWalking(false);
                anim.IsBlocking(false);
                break;
            case State.WALKING:
                anim.IsWalking(true);
                break;
            case State.DEFENDING:
                anim.IsBlocking(true);
                break;
            case State.JUMPING:
                anim.Jump(AnimationTrigger.set);
                break;
           default:
                anim.Jump(AnimationTrigger.reset);
                break;
        }
    }
    #region HitBoxLogic
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
    public void OpenPunchHitBox()
    {
        BoxCollider hitBox = GameObject.FindGameObjectWithTag("Hitbox").GetComponent<BoxCollider>();
        hitBox.enabled = true;
        if(id ==1)
            momentumBar.IsPlayerOne = true;
        else
            momentumBar.IsPlayerOne = false;
        
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
        //rb.velocity += (Vector3.down + transform.forward) * kickVelocity;
    }
    private void CloseKickHitBox() { KickHitBox.enabled = false; }
    //    public void Hit(Vector3 opponentDirection)
    //    {
    //        if (currentState != State.Defending)
    //        {
    //            currentState = State.Hit;
    //            rb.AddForce(opponentDirection * 100.0f);
    //            anim.HitAnimation(true);
    //        }
    //    }
    #endregion


}








