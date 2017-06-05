using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem
{
    public string[] buttons;
    public int index;
    public float inBetweenTime;
    public float lastKeyPressTime;
    public ComboSystem(string[] button)
    {
        index = 0;
        inBetweenTime = 1.5f;
        lastKeyPressTime = 0.0f;
        buttons = button;
    }

    public bool CheckCombo()
    {
        if (Time.time > lastKeyPressTime + inBetweenTime)
        {
            index = 0;
            lastKeyPressTime = Time.time;
            return false;
        }
        else
        {
            if (index < buttons.Length)
            {
                if (Input.GetButtonDown(buttons[index]))
                {
                    lastKeyPressTime = Time.time;
                    index++;
                    if (index >= buttons.Length)
                    {
                        index = 0;
                        return true;
                    }
                    if(index >= 2)
                    {
                        Debug.Log("Third Hit!");
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}

