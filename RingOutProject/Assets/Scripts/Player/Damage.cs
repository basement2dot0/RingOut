using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

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
    void Start ()
    {
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
               break;
            case DamageType.MEDIUM:
                minDamage = 10.0f;
                maxDamage = 15.0f;
                currentDamage = CurrentDamage(minDamage, maxDamage);
                break;
            case DamageType.HEAVY:
                minDamage = 15.0f;
                maxDamage = 20.0f;
                currentDamage = CurrentDamage(minDamage, maxDamage);
                break;
            default:
                Debug.LogError("Please Select a Damage Type");
                break;
        }
    }

    public float CurrentDamage(float minDmg, float maxDmg)
    {
        float dmg = Random.Range(minDamage,maxDmg);
        //Debug.Log("Dmg Output:"+currentDamage.ToString());
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
