using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField]
    private BoxCollider attackHitBox;
    [SerializeField]
    private BoxCollider torso;
    [SerializeField]
    private BoxCollider legs;
    private Rigidbody rb;
    private Vector3 hitDireciton;



    private Player player;
    //Momentum Bar
    private MomentumBar momentumBar;
    private PlayerAnim anim;
    private void Awake()
    {
        momentumBar = GameObject.FindGameObjectWithTag("Canvas").GetComponent<MomentumBar>();
        player = GetComponentInParent<Player>();
        rb = GetComponent<Rigidbody>();
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
        //rb.velocity = direction * 100f;
        //player.transform.position += direction* 20 * Time.time;
        //rb.AddForce(direction * 1000.0f);
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
    public void CloseHitBox()
    {
        attackHitBox.enabled = false;
    }
    #endregion




}
