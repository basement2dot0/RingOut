﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dmg : MonoBehaviour {

    [SerializeField]
    private DamageType damageType;

    [SerializeField]
    private float minDamage;

    [SerializeField]
    private float maxDamage;

    [SerializeField]
    private float currentDamage;

    public float MinDamage { get { return minDamage; }}
    public float MaxDamage { get { return maxDamage; }}

    // Use this for initialization
    void Start () {
        damageType = DamageType.NONE;
        Initialize();
    }
	
    private void Initialize()
    {
        switch (damageType)
        {
            case DamageType.LIGHT:
                //set damage 
                minDamage = 1.0f;
                maxDamage = 10.0f;
                currentDamage = CurrentDamage(minDamage, maxDamage);
                Debug.Log(currentDamage.ToString());
                break;
            case DamageType.MEDIUM:
                minDamage = 10.0f;
                maxDamage = 15.0f;
                currentDamage = CurrentDamage(minDamage, maxDamage);
                Debug.Log(currentDamage.ToString());
                break;
            case DamageType.HEAVY:
                minDamage = 15.0f;
                maxDamage = 20.0f;
                currentDamage = CurrentDamage(minDamage, maxDamage);
                Debug.Log(currentDamage.ToString());
                break;
            default:
                Debug.LogError("Please Select a Damage Type");
                break;
        }
    }

    private float CurrentDamage(float minDmg, float maxDmg)
    {
        float dmg = Random.Range(minDamage,maxDmg);
        return dmg;
    }
}
public enum DamageType
{
    LIGHT,
    MEDIUM,
    HEAVY,
    NONE
}
