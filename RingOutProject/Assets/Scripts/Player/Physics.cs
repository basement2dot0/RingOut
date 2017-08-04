using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Physics : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 velocity = Vector3.zero;
    private Vector3 position;
    private Player player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        velocity = rb.velocity;
        position = transform.position;
    }
    private void LateUpdate()
    {
        UpdatePositon();
    }

    public void KnockedBack(Vector3 hitDirection)
    {
        player.CanMove = false;
        velocity = hitDirection;
        position = new Vector3(position.x + velocity.x, position.y + velocity.y, position.z + velocity.z) * Time.deltaTime;
        rb.velocity = velocity;
    }

    private void UpdatePositon()
    {
        rb.position = position;
    }

    private void Jump()
    {
        if (player.IsJumping)
        {

        }
    }
    private void Gravity() { }
}

