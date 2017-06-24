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
     
        
        if(other.name == "BlockArea"+player.opponent.GetComponent<Player>().ID.ToString())
        {
            
            //negate momentum bar here
            Debug.Log(other.name);
            player.opponent.GetComponent<PlayerAnim>().BlockHit(AnimationTrigger.set);
            TempDisableTorsoHitBox();
            
        }
        else if(other.name == "Body" + player.opponent.GetComponent<Player>().ID.ToString())
        {
            if(!isBlock)
            {
                if (player.isHyped)
                {
                    Debug.Log("IS HI0T");
                    player.opponent.DamageTaken(player.transform.forward);
                    player.opponent.GetComponent<PlayerAnim>().IsHypeHit(true);

                   
                }
                else
                {
                    Debug.Log(other.name);
                    momentumBar.OnHit();
                    player.opponent.GetComponent<PlayerAnim>().Hit(AnimationTrigger.set);
                    
                }
            }
            isBlock = false;
        }
    }
}
