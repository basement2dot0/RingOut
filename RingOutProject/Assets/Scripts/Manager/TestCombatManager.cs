using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Text;


    class TestCombatManager : MonoBehaviour
    {
        private Dictionary<string, string> registeredCombos;
        private InputManager inputManager;
        private StringBuilder activeCombo;
        private Animator anim;
        private bool hasPerformedCombo;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            inputManager = GetComponent<InputManager>();


        }

        private void Start()
        {
            hasPerformedCombo = false;
            activeCombo = new StringBuilder();
            registeredCombos = new Dictionary<string, string>();
            registeredCombos.Add("ComboOne", "PPP");
        }
        private void Update()
        {
            ActionSelect();
            if (hasPerformedCombo)
            {

                hasPerformedCombo = false;
            
            }

        }
        private void ActionSelect()
        {
            if (inputManager.AttackButtonUP(inputManager.controlNo))
            {
                ComboTime.LastInput = Time.time;
                activeCombo.Append("P");
            CheckForCombo();
            Debug.Log("Punch");
                return;
            }

            if (inputManager.DefendButtonUp(inputManager.controlNo))
            {
                ComboTime.LastInput = Time.time;
                activeCombo.Append("Block");
            CheckForCombo();
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
                    Debug.Log("Combo Executed " + item.ToString());
                    hasPerformedCombo = true;
                }

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
            return ((lastInput - Time.time) <= inputTime) ? true : false;
        }
    }
