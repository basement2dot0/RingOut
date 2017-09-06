using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MariePhysics  : Physics
{
    private float lungeDistance = 5.0f;
    private WaitForSeconds delay = new WaitForSeconds(.5f);

    private void Update()
    {
        Lunge();
    }

    private void Lunge()
    {
        if (player.IsAttacking && player.AttackCounter == 0)
            StartCoroutine("DelayLunge");
    }

    IEnumerator DelayLunge()
    {
        yield return delay;
        rb.position += rb.transform.forward * lungeDistance;
    }
}

