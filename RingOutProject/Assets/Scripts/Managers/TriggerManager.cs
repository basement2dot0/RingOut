using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    public abstract class TriggerManager : MonoBehaviour
    {
        protected Player player;
        [SerializeField]
        protected string opponentsBlockArea;
        [SerializeField]
        protected string opponentsBody;

        private void Start()
        {
            player = GetComponentInParent<Player>();
            opponentsBlockArea = "BlockArea" + player.Opponent.ID.ToString();
            opponentsBody = "Body" + player.Opponent.ID.ToString();
        }

        private void OnTriggerEnter(Collider other)
        {
            ActivateTriggers(other);
        }

        protected abstract void ActivateTriggers(Collider opponentHitbox);
         
    }

