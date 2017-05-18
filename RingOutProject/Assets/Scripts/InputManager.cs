using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
   
    public int controlNo;
    //public int ControlNo
    //{ get { return controlNo; } set { controlNo = value; } }
    //private static InputManager instance;
    //public static InputManager Instance
    //{
    //    get
    //    {
    //      if(instance == null)
    //            instance = new InputManager();
    //        return instance;
    //    }
    //}

    public float GetHorizontal(int playerID)
    {  
    return  Input.GetAxis("Horizontal" + playerID.ToString());
    }

    public float GetVertical(int playerID){
        return Input.GetAxis("Vertical" + playerID.ToString());
    }

    public Vector3 Movement(int playerID)
    {
        return new Vector3(GetHorizontal(playerID), 0, GetVertical(playerID));
    }

    public bool AttackButtonDown(int playerID)
    {
        return Input.GetButton("Attack" + playerID.ToString());
    }

    public bool AttackButtonUP(int playerID)
    {
        return Input.GetButtonUp("Attack" + playerID.ToString());
    }

    public bool DefendButtonDown(int playerID)
    {
        return Input.GetButtonDown("Block" + playerID.ToString());
    }

    public bool DefendButtonUp(int playerID)
    {
        return Input.GetButtonUp("Block" + playerID.ToString());
    }

    public bool JumpButtonDown(int playerID)
    {
        return Input.GetButtonDown("Jump" + playerID.ToString());
    }
}
