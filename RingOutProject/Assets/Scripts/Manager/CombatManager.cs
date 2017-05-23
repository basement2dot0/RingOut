using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum Action
{
    BLOCK,
    PUNCH,
    KICK,
    NONE
}
public class CombatManager : MonoBehaviour
{

    [SerializeField]
    private int maxCombo = 2;
    private int currentCombo = 0;
    private List<string> combo = new List<string>();
    private Action comboAttack = Action.NONE;
    private Animator anim;
    private string lastString;
    private List<string> ci;
    private string[] combos;
    private InputManager inputManager;
   

    private void Awake()
    {
        anim = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();

        
    }
    private void Start()
    {
        ci = new List<string>();
        combos = new string[2];
        combos[0] = "PunchPunchPunch";
        combos[1] = "KickKickKick";
    }
    private void Update()
    {

        ActionSelect();
        ComboStrings(combos);
    }
    
    public bool ComboStrings(string[] combos)
    {
        int combinationStart = -1;
        for (int i = 0; i < ci.Count; i++)
        {
            if (combinationStart >= 0)
            {
                if (i - combinationStart >= combos.Length)
                {
                    Debug.Log("Combo Successful!");
                    return true;
                }

                if (ci[i] != combos[i - combinationStart])
                {
                    Debug.Log("Combo Unsuccessful.");
                    combinationStart = -1;
                }
            }
            else
            {
                if (i + combos.Length >= ci.Count)
                    return false;

                if (ci[i] == combos[0])
                    combinationStart = i;
            }
        }
        Debug.Log("HELLO WORLD!");
        return true;
    }
    public void PlayerInput()
    {

        switch (comboAttack)
        {
            case Action.BLOCK:
                Comboing("Block");
                break;
            case Action.PUNCH:
                Comboing("Punch");
                break;
            case Action.KICK:
                Comboing("Kick");
                break;
            default:
                break;
        }
    }
    private void Comboing(string comboString)
    {


        if (currentCombo < maxCombo)
        {

            combo.Add(comboString);
            anim.Play(comboString);
            currentCombo++;

        }
        else if (ComboTime.CanCombo())
        {
            lastString = comboString;
            //ComboStrings(ci);
            currentCombo = 0;
            combo.Clear();


        }
    }
    private void ActionSelect()
    {
        if (inputManager.AttackButtonUP(inputManager.controlNo))
        {
            ComboTime.LastInput = Time.deltaTime;
            comboAttack = Action.PUNCH;
            PlayerInput();
            ci.Add("Punch");
            Debug.Log("Punch");
            return;
        }
        
        if (inputManager.DefendButtonUp(inputManager.controlNo))
        {
            ComboTime.LastInput = Time.deltaTime;
            comboAttack = Action.BLOCK;
            PlayerInput();
            ci.Add("Block");
            Debug.Log("Block");
            return;
        }
    }

}

public static class ComboTime
{
    private static float inputTime = 0.5f;
    private static float lastInput;

    public static float LastInput
    {
        get { return lastInput; }
        set { lastInput = value; }

    }
    public static float InputTime
    {
        get { return inputTime; }

    }
    public static bool CanCombo()
    {
        return ((lastInput - Time.deltaTime) <= inputTime) ? true : false;
    }
}

