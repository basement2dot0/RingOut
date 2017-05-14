using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private bool hitBoxOpen;
    [SerializeField]
    private BoxCollider hitBox;
    private PlayerOne playerone;
    private Vector3 punchForce;
    

    private void Awake()
    {
        hitBox.enabled = false;
            playerone = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerOne>();
        punchForce = Vector3.one * 5;
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerTwo opponenet;
        if (other.gameObject.tag == "PlayerTwo")
        {
            opponenet = other.gameObject.GetComponent<PlayerTwo>();
            opponenet.Hit(playerone.transform.forward);
        }

    }
   
    
}
