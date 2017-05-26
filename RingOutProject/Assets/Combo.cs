using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{

    private ComboSystem Hadouken;
    private InputManager inputManager;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();

        Hadouken = new ComboSystem(new string[] { "Jump" + inputManager.controlNo , "Jump" + inputManager.controlNo, "Jump" + inputManager.controlNo});
    }

    void Update ()
    {
        if (Hadouken.CheckCombo())
        {
            Debug.Log(inputManager.controlNo + " HADOUKEN!");
        }
	}
}
