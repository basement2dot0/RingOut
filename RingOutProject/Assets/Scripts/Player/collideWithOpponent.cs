using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collideWithOpponent : MonoBehaviour
{
    private MomentumBar momentumBar;
    private Player player;
    private WaitForSeconds disableHitboxTime;
    [SerializeField]
    public float wait;
    private void Awake()
    {
        disableHitboxTime = new WaitForSeconds(wait);
        player = GetComponentInParent<Player>();
        momentumBar = GameObject.FindGameObjectWithTag("Canvas").GetComponent<MomentumBar>();
        
    }
    bool isBlock;
    private void TempDisableTorsoHitBox()
    {
        isBlock = true;
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
     
        
        if(other.name == "BlockArea"+player.otherPlayer.ID.ToString())
        {
            
            //negate momentum bar here
            Debug.Log(other.name);
            player.otherPlayer.IsHit = true;
            //player.opponent.GetComponent<PlayerAnim>().BlockHit(AnimationTrigger.set);
            TempDisableTorsoHitBox();
            
        }
        else if(other.name == "Body" + player.otherPlayer.ID.ToString())
        {
            if(!isBlock)
            {
                if (player.isHyped)
                {
                    Debug.Log("IS HIT");
                    player.otherPlayer.IsHypeHit = true;
                    player.opponent.DamageTaken(player.transform.forward);
                    player.opponent.GetComponent<PlayerAnim>().IsHypeHit(true);

                   
                }
                else
                {
                    Debug.Log(other.name);
                    momentumBar.UpdateBar();
                    //player.opponent.GetComponent<PlayerAnim>().Hit(AnimationTrigger.set);
                    player.otherPlayer.IsHit = true;

                }
            }
            isBlock = false;
        }
    }
}
