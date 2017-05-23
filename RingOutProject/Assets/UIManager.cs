using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UIManager : MonoBehaviour
{
    private Image highlight;

    void Awake()
    {
        highlight = GameObject.FindGameObjectWithTag("Highlight").GetComponent<Image>();
    }


    public void Pressed()
    {
        Debug.Log("Clicked");
    }

   
}
