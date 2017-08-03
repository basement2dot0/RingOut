using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField]
    private BoxCollider jumpAttackHitBox;
    [SerializeField]
    private BoxCollider attackHitBox;
    [SerializeField]
    private BoxCollider torso;
    [SerializeField]
    private Vector3 hitDireciton;
    [SerializeField]
    private BoxCollider blockArea;
    //Player Reference
    private Player player;
    //Momentum Bar Reference
    private MomentumBar momentumBar;
    //delay between active attacks
    private WaitForSeconds delay = new WaitForSeconds(.50f);
    private Movement playerMovement;

    public Vector3 HitDireciton
    {
        get { return hitDireciton; }
        set { hitDireciton = value; }
    }
    public BoxCollider Torso
    {
        get {return torso; }
        set {torso = value; }
    }

    

    



    private void Awake()
    {
        momentumBar = GameObject.FindGameObjectWithTag("Canvas").GetComponent<MomentumBar>();
        player = GetComponentInParent<Player>();
        torso.name += player.ID.ToString();
        blockArea.name += player.ID.ToString();
    }
    private void Update()
    {
       
    }
    #region HitBoxLogic
    public void DamageTaken(Vector3 direction)
    {
        Debug.Log("IS HIT");
        //player.CurrentState = State.HIT;
        //player.IsHit = true;
        hitDireciton = direction;
    }
    
    private void OpenHitBox()
    {
        BoxCollider AttackHitBox = attackHitBox;
        player.CanMove = false;
        if (player.IsGrounded)
            AttackHitBox = attackHitBox;
        else
            AttackHitBox = jumpAttackHitBox;
        AttackHitBox.enabled = true;
        if (!player.isHyped)
        {
            if (player.ID == 1)
                momentumBar.IsPlayerOne = true;
            else
                momentumBar.IsPlayerOne = false;
        }
        else
            StartCoroutine("setHypeFalse");
    }
    private IEnumerator setHypeFalse()
    {
        yield return delay;
        player.isHyped = false;
    }
 
    private void CloseHitBox()
    {
        attackHitBox.enabled = false;
        player.CanMove = true;

    }
    private void OpenBlockArea()
    {
        blockArea.enabled = true;
        player.CanMove = false;
    }
    private void CloseBlockArea()
    {
        blockArea.enabled = false;
        player.CanMove = true;
    }
    #endregion
}
