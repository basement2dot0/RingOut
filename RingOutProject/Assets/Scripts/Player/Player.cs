using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Combat))]
[RequireComponent(typeof(DamageType))]
[RequireComponent(typeof(AudioManager))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(AnimationManager))]
public class Player : MonoBehaviour{
    
    //Universal Player variables
    [SerializeField]
    private int id;
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private bool isHyped;
    [SerializeField]
    private bool isJumping;
    [SerializeField]
    private bool canMove;
    [SerializeField]
    private bool isHit;
    [SerializeField]
    private bool isDefending;
    [SerializeField]
    private bool isAttacking;
    [SerializeField]
    private Player opponent;
    [SerializeField]
    private bool isWalking;
    [SerializeField]
    private bool isHypeHit;
    [SerializeField]
    private bool hypeAttack;
    [SerializeField]
    private bool isKnockedBack;
    [SerializeField]
    private bool isPushed;
    [SerializeField]
    private int attackCounter;
    private Vector3 hitDirection;
    private DamageType damageType;

    //Public Properties 
    public int ID { get { return id; } }
    public Player Opponent { get { return opponent; }}
    public bool IsGrounded { get { return isGrounded; } }
    public Vector3 HitDirection
    {
        get { return hitDirection; }
        set { hitDirection = value; }
    }
    public int AttackCounter
    {
        get { return attackCounter; }
        set { attackCounter = value; }
    }
    public bool IsHit
    {
        get { return isHit; }
        set {isHit = value; }
    }
    public bool IsDefending
    {
        get { return isDefending; }
        set { isDefending = value; }
    }
    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }
    public bool IsWalking
    {
        get { return isWalking; }
        set { isWalking = value; }
    }
    public bool IsHypeHit
    {
        get { return isHypeHit; }
        set { isHypeHit = value; }
    }
    public bool HypeAttack
    {
        get { return hypeAttack; }
        set { hypeAttack = value; }
    }
    public bool IsJumping
    {
        get { return isJumping; }
        set { isJumping = value; }
    }
    public bool IsAttacking
    {
        get { return isAttacking; }
        set { isAttacking = value; }
    }
    public bool IsHyped
    {
        get { return isHyped; }
        set { isHyped = value; }
    }
    public bool IsKnockedBack
    {
        get { return isKnockedBack; }
        set { isKnockedBack = value; }
    }
    public bool IsPushed
    {
        get { return isPushed; }
        set { isPushed = value; }
    }
    public DamageType DamageType
    {
        get { return damageType; }
        set { damageType = value; }
    }

    //Unity Methods
    private void Awake()
    {
        canMove = true;
        foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (item.GetComponent<Player>() != this)
                opponent = item.GetComponent<Player>();
        }
        id = GetComponent<InputManager>().ControlNo;
        damageType = GetComponent<DamageType>();
    }
    private void Update()
    {
        //anim.IsFalling();
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