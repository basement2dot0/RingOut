using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private bool hitBoxOpen;
    [SerializeField]
    private BoxCollider hitBox;
    

    private void Awake()
    {
        hitBox.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerTwo")
        {
            DoDmg(5);
        }
    }
    private void DoDmg(int amount)
    {
        Debug.Log("Hit for " + amount.ToString() + "!");
    }
    
}
