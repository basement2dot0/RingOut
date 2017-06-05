using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{

    private ComboSystem Punch;
    private InputManager inputManager;
    private Damage damage;
    private Animator anim;
    private Player player;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        damage = GetComponent<Damage>();
        Punch = new ComboSystem(new string[] { "Jump" + inputManager.controlNo });
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    void Update ()
    {
        if (Punch.CheckCombo() && player.isHyped)
        {
            Debug.Log("HYPE ATTACK!" + inputManager.controlNo);
            anim.Play("HypeAttack");
        }
        else if (Punch.CheckCombo())
        {
            Debug.Log("Punch" + inputManager.controlNo);
        }
    }
}
