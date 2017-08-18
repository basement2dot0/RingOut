using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TorsoTrigger : TriggerManager
{
    InputManager inputManager;
    private void Awake()
    {
        inputManager = GetComponentInParent<InputManager>();
    }
    protected override void ActivateTriggers(Collider hitbox)
    {
        if(hitbox.name == opponentsBody)
            player.IsPushed = true;
        else
            player.IsPushed = false;
    }

}

