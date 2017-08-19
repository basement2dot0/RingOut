using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    class DefenseTrigger : TriggerManager
    {

        protected override void ActivateTriggers(Collider hitbox)
        {   
            if (hitbox.name == opponentDefenseHitbox)
                player.Opponent.IsHit = true;
        }

    
    }

