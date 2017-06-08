using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class CameraController : MonoBehaviour
{
    [SerializeField]
    private Player leftTarget;

    [SerializeField]
    private float fov;

    [SerializeField]
    private Player rightTarget;

    [SerializeField]
    private GameObject center;

    [SerializeField]
    private float zoomIn;
    [SerializeField]
    private float defaultZoom;

    private float lastFrameTime;
    private float myDeltaTime;

    private void Awake()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach(var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponent<Player>().ID == 1)
                leftTarget = player.GetComponent<Player>();
            else
                rightTarget = player.GetComponent<Player>();
        }
        defaultZoom = Camera.main.fieldOfView;
        lastFrameTime = Time.realtimeSinceStartup;
        fov = 10;
        zoomIn = 50.0f;
    }
    private void Update()
    {
        CenterFocus();
    }

    private void LateUpdate()
    {
        myDeltaTime = Time.realtimeSinceStartup - lastFrameTime;
        lastFrameTime = Time.realtimeSinceStartup;
    }

    /// <summary>
    /// Keeps the camera constantly focused on the Center GameObject
    /// </summary>
    private void CenterFocus()
    {
        if (leftTarget.CurrentState == State.HIT)
        {
            center.transform.position = leftTarget.transform.position;
            Camera.main.transform.LookAt(center.transform);
            Camera.main.fieldOfView = (defaultZoom + zoomIn);
        }
        if (rightTarget.CurrentState == State.HIT)
        {
            center.transform.position = rightTarget.transform.position;
            Camera.main.transform.LookAt(center.transform);
            if (Camera.main.fieldOfView >= fov)
                Camera.main.fieldOfView = fov;
            Time.timeScale = 1;
        }
        else
        {
            center.transform.position = (leftTarget.transform.position + rightTarget.transform.position) / 2;
            Camera.main.transform.LookAt(center.transform);
        }
    }
}

