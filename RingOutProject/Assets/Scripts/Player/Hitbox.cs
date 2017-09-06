using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField]
    private BoxCollider hypeAttackHitBox;
    [SerializeField]
    private BoxCollider attackHitBox;
    [SerializeField]
    private BoxCollider torso;
    [SerializeField]
    private BoxCollider blockArea;
    private BoxCollider ground;
    //Player Reference
    private Player player;
   //delay between active attacks
    [SerializeField]
    private WaitForSeconds delay;

    public BoxCollider Torso
    {
        get {return torso; }
        set {torso = value; }
    }

    private void Awake()
    {
        delay = new WaitForSeconds(.50f);
        player = GetComponentInParent<Player>();
        torso.name += player.ID.ToString();
        blockArea.name += player.ID.ToString();
    //    torso.gameObject.AddComponent<TorsoTrigger>();
    //    blockArea.gameObject.AddComponent<DefenseTrigger>();
    //    attackHitBox.gameObject.AddComponent<AttackTrigger>();
    //    hypeAttackHitBox.gameObject.AddComponent<HypeAttackTrigger>();
    }
    private void LateUpdate()
    {
        CloseBlockArea();
        
    }

    #region HitBoxLogic
    private void OpenHitBox()
    {
        if (player.IsGrounded)
        {
            player.CanMove = false;
            attackHitBox.enabled = true;
            UpdateAttackCounter();
        }
        else
            hypeAttackHitBox.enabled = true;
        StartCoroutine("CloseHitBoxes");
        
    }
    private void CloseHitBox()
    {
        attackHitBox.enabled = false;
        hypeAttackHitBox.enabled = false;
        player.CanMove = true;
    }
    private void OpenBlockArea()
    {
        blockArea.enabled = true;
        player.CanMove = false;
    }
    private void CloseBlockArea()
    {
        if(!player.IsDefending)
        blockArea.enabled = false;
        player.CanMove = true;
    }
    private void OpenHypeHitBox()
    {
        if (player.IsGrounded)
        {
            player.CanMove = false;
            hypeAttackHitBox.enabled = true;
        }
        

    }
    private IEnumerator CloseHitBoxes()
    {
        yield return delay;
        CloseHitBox();
    }
    private void UpdateAttackCounter()
    {
        switch (player.AttackCounter)
        {
            case 0:
                player.AttackCounter = 1;
                break;
            case 1:
                player.AttackCounter = 2;
                break;
            case 2:
                player.AttackCounter = 3;
                break;
            case 3:
                player.AttackCounter = 1;
                break;
            default:
                player.AttackCounter = 1;
                break;
        }
        player.LastSuccessfulAttack = Time.time;
    }

    #endregion
}
