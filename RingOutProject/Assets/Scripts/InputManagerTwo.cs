using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerTwo : MonoBehaviour
{
    private static InputManagerTwo instance;
    public static InputManagerTwo Instance
    {
        get
        {
          if(instance == null)
                instance = new InputManagerTwo();
            return instance;
        }
    }

    public float GetHorizontal(){
        return Input.GetAxis("Horizontal2");}

    public float GetVertical(){
        return Input.GetAxis("Vertical2");}

    public Vector3 Movement(){
        return new Vector3(GetHorizontal(), 0, GetVertical());}

    public bool AttackButtonDown(){
        return Input.GetButtonDown("Fire12");}

    public bool AttackButtonUP(){
        return Input.GetButtonUp("Fire12");}

    public bool DefendButtonDown(){
        return Input.GetButtonDown("Fire22");}

    public bool DefendButtonUp(){
        return Input.GetButtonUp("Fire22");}

    public bool GrabButtonDown(){
        return Input.GetButtonDown("Jump2");}

    public bool GrabButtonUp(){
        return Input.GetButtonUp("Fire3");}


}
