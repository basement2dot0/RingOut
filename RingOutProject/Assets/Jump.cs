using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {
    [SerializeField]
    [Range(0, 100)]
    private float jumpVelocity;

    private float fallMultiplyer;
    private float lowJumpMultiplyer;
    
    public void JumpInput(Rigidbody rb)
    {
        if (InputManager.Instance.GrabButtonDown())
        {
            Debug.Log("Neutral  Jump");
            
            rb.AddForce(Vector3.up * jumpVelocity);
        }

    }
}
