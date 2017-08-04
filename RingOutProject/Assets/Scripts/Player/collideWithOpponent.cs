using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collideWithOpponent : MonoBehaviour
{
    private MomentumBar momentumBar;
    private Player player;
    private WaitForSeconds disableHitboxTime;
    private bool isBlock;
    [SerializeField]
    public float wait;
    private void Awake()
    {
        disableHitboxTime = new WaitForSeconds(wait);
        player = GetComponentInParent<Player>();
        momentumBar = GameObject.FindGameObjectWithTag("Canvas").GetComponent<MomentumBar>();
    }
    
    private void TempDisableTorsoHitBox()
    {
        isBlock = true;
    }
    private void OnTriggerEnter(Collider other)
    {
     
        
        if(other.name == "BlockArea"+player.OtherPlayer.ID.ToString())
        {
            //negate momentum bar here
            //Debug.Log(other.name);
            TempDisableTorsoHitBox();
            
        }
        else if(other.name == "Body" + player.OtherPlayer.ID.ToString())
        {
            if(!isBlock)
            {
                if (player.IsHyped)
                {
                   // Debug.Log("IS HIT");
                    player.OtherPlayer.IsHypeHit = true;
                    player.Opponent.DamageTaken(player.transform.forward);
                }
                else
                {
                    //Debug.Log(other.name);
                    momentumBar.UpdateBar();
                    player.OtherPlayer.IsHit = true;
                }
            }
            isBlock = false;
        }
    }
}
