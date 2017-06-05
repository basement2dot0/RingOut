using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{

    private ComboSystem Hadouken;
    private InputManager inputManager;
    private Damage damage;
    private Animator anim;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        damage = GetComponent<Damage>();
        Hadouken = new ComboSystem(new string[] { "Jump" + inputManager.controlNo });

        anim = GetComponent<Animator>();

    }

    void Update ()
    {
        if (Hadouken.CheckCombo())
        {
            //Debug.Log(inputManager.controlNo + " HADOUKEN!");
            anim.Play("Punch");
        }
	}
}
