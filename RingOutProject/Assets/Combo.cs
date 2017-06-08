using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    private InputManager inputManager;
    private PlayerAnim anim;
    private Player player;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        anim = GetComponent<PlayerAnim>();
        player = GetComponent<Player>();
    }
    void Update ()
    {
        CheckForCombo();

    }
    private void CheckForCombo()
    {
        if (player.isHyped && inputManager.AttackButtonDown(player.ID))
        {
            Debug.Log("HYPE ATTACK!" + inputManager.controlNo);
            anim.PlayHypeAttack(true);
            player.currentState = State.Attacking;
        }
        if (inputManager.AttackButtonDown(player.ID))
        {
            player.currentState = State.Attacking;
            Debug.Log("Attack!");
            //call players unique property
            anim.PlayAttack();

            
        }
        else if (inputManager.AttackButtonUp(player.ID))
        {

            player.currentState = State.Idle;
            anim.StopAttack();
            
        }

    }
    

    
}
