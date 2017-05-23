using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Text;

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
    private Dictionary<string,string> registeredCombos;
    private string[] combos;
    private InputManager inputManager;
    private StringBuilder activeCombo;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();

        
    }
    private void Start()
    {
        activeCombo = new StringBuilder();
        combos = new string[2];
        registeredCombos = new Dictionary<string, string>();
        registeredCombos.Add("ComboOne", "PPP");
    }
    private void Update()
    {

        ActionSelect();
        //CheckForCombo();
        //ComboStrings(combos);
    }
    
    
    public void PlayerInput()
    {

        switch (comboAttack)
        {
            case Action.BLOCK:
                Comboing("Block");
                break;
            case Action.PUNCH:
                Comboing("P");
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
            //ComboStrings(combos);
            CheckForCombo();
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
            activeCombo.Append("P");
            Debug.Log("Punch");
            return;
        }
        
        if (inputManager.DefendButtonUp(inputManager.controlNo))
        {
            ComboTime.LastInput = Time.deltaTime;
            comboAttack = Action.BLOCK;
            PlayerInput();
            activeCombo.Append("Block");
            Debug.Log("Block");
            return;
        }
    }

    private void CheckForCombo()
    {
        foreach (var item in registeredCombos.Keys)
        {
            var comboButton = registeredCombos[item];
            if (activeCombo.ToString().Contains(comboButton))
            {
                Debug.Log("Combo Executed" + item.ToString());
            }
        }
    }

}



