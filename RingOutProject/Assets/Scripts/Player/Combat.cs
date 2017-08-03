using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private InputManager inputManager;
    private PlayerAnim anim;
    private Player player;
    [SerializeField]
    private float attackDelay;
    private WaitForSeconds delay;
    
    private void Awake()
    {
        delay = new WaitForSeconds(attackDelay);
        inputManager = GetComponent<InputManager>();
        anim = GetComponent<PlayerAnim>();
        player = GetComponent<Player>();
    }
    void Update()
    {
        Attack();
        Block();
    }
    
    private void Attack()
    {
        if (player.isHyped && inputManager.AttackButtonDown(player.ID))
            anim.AttackIsHyped(true);
        else if (inputManager.AttackButtonDown(player.ID))
        {
            player.isAttacking = true;
            StartCoroutine("inputDelay");
        }

        }
    private void Block()
    {
        if (player.IsGrounded)
        {
            if (inputManager.DefendButton(player.ID))
                player.IsDefending = true;
            else if (!inputManager.DefendButton(player.ID))
                player.IsDefending = false;
            
        }
    }
    
    private IEnumerator inputDelay()
    {
        
        yield return delay;
        player.isAttacking = false;
        
    }
}
