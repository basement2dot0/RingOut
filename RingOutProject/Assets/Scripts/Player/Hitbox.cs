using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField]
    private BoxCollider attackHitBox;
    [SerializeField]
    public BoxCollider torso;
    [SerializeField]
    private Vector3 hitDireciton;
    [SerializeField]
    private BoxCollider blockArea;



    private Player player;
    //Momentum Bar
    private MomentumBar momentumBar;
    private void Awake()
    {
        momentumBar = GameObject.FindGameObjectWithTag("Canvas").GetComponent<MomentumBar>();
        player = GetComponentInParent<Player>();
        torso.name += player.ID.ToString();
        blockArea.name += player.ID.ToString();
        
    }
    private void Update()
    {
        OnHit();
    }
    #region HitBoxLogic
    public void DamageTaken(Vector3 direction)
    {

        player.CurrentState = State.HIT;
        player.IsHit = true;
        hitDireciton = direction;
    }
    private void OnHit()
    {
        if (player.IsHit)
        {
            player.transform.position += hitDireciton * 500 * Time.deltaTime;
        }
    }
    private void OpenHitBox()
    {
        attackHitBox.enabled = true;
        if (player.ID == 1)
            momentumBar.IsPlayerOne = true;
        else
            momentumBar.IsPlayerOne = false;
    }
    private void CloseHitBox()
    {
        attackHitBox.enabled = false;
    }
    private void OpenBlockArea()
    {
        blockArea.enabled = true;
    }
    private void CloseBlockArea()
    {
        blockArea.enabled = false;
    }
    #endregion




}
