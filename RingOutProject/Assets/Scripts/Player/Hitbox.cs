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
    private BoxCollider blockArea;
    //Player Reference
    private Player player;
    //Momentum Bar Reference
    private MomentumBar momentumBar;
    //delay between active attacks
    private WaitForSeconds delay;

    public BoxCollider Torso
    {
        get {return torso; }
        set {torso = value; }
    }

    private void Awake()
    {
        momentumBar = GameObject.FindGameObjectWithTag("Canvas").GetComponent<MomentumBar>();
        delay = new WaitForSeconds(.50f);
        player = GetComponentInParent<Player>();
        torso.name += player.ID.ToString();
        blockArea.name += player.ID.ToString();
    }

    #region HitBoxLogic
    private void OpenHitBox()
    {
        if (player.IsGrounded)
        {
            player.CanMove = false;
            attackHitBox.enabled = true;
        }
        else
            jumpAttackHitBox.enabled = true; 
        
        //if (!player.IsHyped)
        //{
        //    if (player.ID == 1)
        //        momentumBar.IsPlayerOne = true;
        //    else
        //        momentumBar.IsPlayerOne = false;
        //}
        //else
        //    StartCoroutine("SetHypeFalse");
    }
    private void CloseHitBox()
    {
        attackHitBox.enabled = false;
        jumpAttackHitBox.enabled = false;
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

    //private IEnumerator SetHypeFalse()
    //{
    //    yield return delay;
    //    player.IsHyped = false;
    //}
    #endregion
}
