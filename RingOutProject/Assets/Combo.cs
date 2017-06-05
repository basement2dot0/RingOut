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
    private Animator anim;
    private Player player;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        damage = GetComponent<Damage>();
        AttackOne = new ComboSystem(new string[] { "Jump" + inputManager.controlNo });
        AttackTwo = new ComboSystem(new string[] { "Jump" + inputManager.controlNo, "Jump" + inputManager.controlNo });
        AttackThree = new ComboSystem(new string[] { "Jump" + inputManager.controlNo, "Jump" + inputManager.controlNo, "Jump" + inputManager.controlNo });

        anim = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    void Update ()
    {
        if (AttackOne.CheckCombo() && player.isHyped)
        {
            Debug.Log("HYPE ATTACK!" + inputManager.controlNo);
            anim.Play("HypeAttack");
        }
        else if (AttackThree.CheckCombo())
        {
            Debug.Log("Attack Three!");
            //call players unique property
             anim.Play("Punch");
        }
        else if (AttackTwo.CheckCombo())
        {
            Debug.Log("Attack Two!");
            //call players unique property
            anim.Play("Punch");
        }
        else  if (AttackOne.CheckCombo())
        {
            //call players unique property
            Debug.Log("Punch" + inputManager.controlNo);
            anim.Play("Attack");
        }
        

    }
}
