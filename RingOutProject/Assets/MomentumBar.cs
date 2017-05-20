using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MomentumBar : MonoBehaviour
{
  
    private Slider momentumBar;
    [SerializeField]
    private bool isPlayerOne; 
    private void Awake()
    {
        momentumBar = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
        
    }

    public void OnHit(float amount)
    {
        if (isPlayerOne)
        {
            momentumBar.value += Mathf.Lerp(momentumBar.value, momentumBar.minValue,amount);


        }
        else
        {
            momentumBar.value -= Mathf.Lerp(momentumBar.value, momentumBar.maxValue, amount);
        }
    }
}
