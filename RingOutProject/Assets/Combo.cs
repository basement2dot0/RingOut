using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    
    private ComboSystem AttackOne;

    private ComboSystem AttackTwo;

    private ComboSystem AttackThree;
    private InputManager inputManager;
    private Damage damage;
    private Animator anim;
    private Player player;

    //frame data
    [SerializeField]
    private float AttackOneFrameLength;
    [SerializeField]
    private float AttackTwoFrameLength;
    [SerializeField]
    private float AttackThreeFrameLength;

    private float delay;
    
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        damage = GetComponent<Damage>();
        AttackOne = new ComboSystem(new string[] { "Jump" + inputManager.controlNo });
        AttackTwo = new ComboSystem(new string[] { "Jump" + inputManager.controlNo, "Jump" + inputManager.controlNo });
        AttackThree = new ComboSystem(new string[] { "Jump" + inputManager.controlNo, "Jump" + inputManager.controlNo, "Jump" + inputManager.controlNo });

        
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    void Update ()
    {
        CheckForCombo();

    }
    private void CheckForCombo()
    {
        if (AttackOne.CheckCombo() && player.isHyped)
        {
            Debug.Log("HYPE ATTACK!" + inputManager.controlNo);
            anim.Play("HypeAttack");
        }
        else if ((Time.deltaTime - AttackOneFrameLength) <= delay && AttackThree.CheckCombo())
        {
            Debug.Log("Attack Three!");
            //call players unique property
            anim.Play("Attack3");
            delay = FrameDelay(AttackThreeFrameLength);
        }
        else if ((Time.deltaTime - AttackOneFrameLength) <= delay && AttackTwo.CheckCombo())
        {
            Debug.Log("Attack Two!");
            //call players unique property
            anim.Play("Attack2");
            delay = FrameDelay(AttackTwoFrameLength);
        }
        else if ((Time.deltaTime - AttackThreeFrameLength) <= delay && AttackOne.CheckCombo())
        {
            //call players unique property
            Debug.Log("Punch" + inputManager.controlNo);
            anim.Play("Attack");
            delay = FrameDelay(AttackOneFrameLength);
        }
    }

    private float FrameDelay(float delay)
    {

        Debug.Log("Frame Delayed:" + delay.ToString());
        return delay;
    }
}
