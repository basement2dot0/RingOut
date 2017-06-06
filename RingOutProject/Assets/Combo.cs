using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    
    private ComboSystem AttackOne;

    private ComboSystem AttackTwo;

    private ComboSystem AttackThree;
    private InputManager inputManager;
    private Damage damage;
    private PlayerAnim anim;
    private Player player;

    //frame data
    [SerializeField]
    private float AttackOneFrameLength;
    [SerializeField]
    private float AttackTwoFrameLength;
    [SerializeField]
    private float AttackThreeFrameLength;

    private float delay;
    private float lastInput;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        //damage = GetComponent<Damage>();
        //AttackOne = new ComboSystem(new string[] { "Jump" + inputManager.controlNo });
        //AttackTwo = new ComboSystem(new string[] { "Jump" + inputManager.controlNo, "Jump" + inputManager.controlNo });
        //AttackThree = new ComboSystem(new string[] { "Jump" + inputManager.controlNo, "Jump" + inputManager.controlNo, "Jump" + inputManager.controlNo });
        
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
        if (inputManager.AttackButtonDown(player.ID) && (Time.deltaTime - lastInput) >= delay)
        {
            Debug.Log("Attack!");
            //call players unique property
            anim.PlayAttack(true);
            player.currentState = State.Attacking;
            lastInput = Time.deltaTime;
        }
        else if (inputManager.AttackButtonUP(player.ID))
        {
            anim.PlayAttack(false);
            player.currentState = State.Idle;
        }

    }

    
}
