using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class CameraController : MonoBehaviour
{
    [SerializeField]
    private PlayerOne leftTarget;

    [SerializeField]
    private PlayerTwo rightTarget;

    [SerializeField]
    private GameObject center;

    [SerializeField]
    private float zoomIn;
    [SerializeField]
    private float defaultZoom;

    private void Awake()
    {
        leftTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerOne>();
        rightTarget = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<PlayerTwo>();
        defaultZoom = Camera.main.fieldOfView;
        zoomIn = 20.0f;
    }
    private void Update()
    {
        CenterFocus();
        
    }

    /// <summary>
    /// Keeps the camera constantly focused on the Center GameObject
    /// </summary>
    private void CenterFocus()
    {
        if (leftTarget.currentState == State.Hit)
        {
            center.transform.position = leftTarget.transform.position;
            Camera.main.transform.LookAt(center.transform);
            Camera.main.fieldOfView = zoomIn; 

        }
        if (rightTarget.currentState == State.Hit)
        {
            center.transform.position = rightTarget.transform.position;
            Camera.main.transform.LookAt(center.transform);
            Camera.main.fieldOfView = zoomIn;
        }
        else
        {
            center.transform.position = (leftTarget.transform.position + rightTarget.transform.position) / 2;
            Camera.main.transform.LookAt(center.transform);
        }
    }
}

