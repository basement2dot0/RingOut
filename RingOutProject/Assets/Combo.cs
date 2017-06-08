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
    private IEnumerator CloseAttack()
    {
        yield return null;
        anim.PlayAttack(false);
    }
    private void CheckForCombo()
    {
        if (player.isHyped && inputManager.AttackButtonDown(player.ID))
        {
            Debug.Log("HYPE ATTACK!" + inputManager.controlNo);
            anim.PlayHypeAttack(true);
            player.currentState = State.Attacking;
        }
        if (inputManager.AttackButtonDown(player.ID) )
        {
            anim.PlayAttack(true);
            StartCoroutine("CloseAttack");
            player.currentState = State.Attacking;
        }
        

        

    }
}
