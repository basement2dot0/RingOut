using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance
    {
        get
        {
          if(instance == null)
                instance = new InputManager();
            return instance;
        }
    }

    public float GetHorizontal(){
        return Input.GetAxis("Horizontal");}

    public float GetVertical(){
        return Input.GetAxis("Vertical");}

    public Vector3 Movement(){
        return new Vector3(GetHorizontal(), 0, GetVertical());}

    public bool AttackButtonDown(){
        return Input.GetButtonDown("Fire1");}

    public bool AttackButtonUP(){
        return Input.GetButtonUp("Fire1");}

    public bool DefendButtonDown(){
        return Input.GetButtonDown("Fire2");}

    public bool DefendButtonUp(){
        return Input.GetButtonUp("Fire2");}

    public bool GrabButtonDown(){
        return Input.GetButtonDown("Jump");}

    public bool GrabButtonUp(){
        return Input.GetButtonUp("Fire3");}


}
