using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private bool canPunch;
    private bool Punching;
    private Collider hitBox;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerTwo")
        {
            Debug.Log("OW!");
        }
    }
}
