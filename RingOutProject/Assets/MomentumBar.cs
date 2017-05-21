using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MomentumBar : MonoBehaviour
{
  
    private Slider momentumBar;
    [SerializeField]
    private bool isPlayerOne;

    private Dmg damage;
    private void Awake()
    {
        momentumBar = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<Dmg>();
        
    }

    public void OnHit()
    {
        if (isPlayerOne)
        {
            if ((momentumBar.value + damage.CurrentDamage(damage.MinDamage, damage.MaxDamage)) > momentumBar.maxValue)
            {
                momentumBar.value = momentumBar.maxValue;
            }
            else
            {
                momentumBar.value += damage.CurrentDamage(damage.MinDamage, damage.MaxDamage);
            }
        }
        else
        {
            if ((momentumBar.value - damage.CurrentDamage(damage.MinDamage, damage.MaxDamage)) < momentumBar.minValue)
            {
                momentumBar.value = momentumBar.minValue;
            }
            else
            {
                momentumBar.value -= damage.CurrentDamage(damage.MinDamage, damage.MaxDamage);
            }
        }
        
    }
}
