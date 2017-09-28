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
    private Vector3 defaultCameraPosition;

    private void Awake()
    {
        defaultCameraPosition = Camera.main.transform.position;
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
        HypeHitFocus();
        PlayerHypedFocus();
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
            Camera.main.transform.position = defaultCameraPosition;
            Camera.main.transform.parent = null;
            Camera.main.orthographic = true;
            center.transform.position = (leftTarget.transform.position + rightTarget.transform.position) / 2;
            Camera.main.transform.LookAt(center.transform);
        
    }
    private void HypeHitFocus()
    {
        if (leftTarget.IsHypeHit)
        {
            PositionHypeHitCamera(leftTarget.transform);
        }
        if (rightTarget.IsHypeHit)
        {
            PositionHypeHitCamera(rightTarget.transform);
        }
    }
    private void PlayerHypedFocus()
    {
        if(leftTarget.IsTaunting)
        {
            PositionHypeCamera(leftTarget.transform);
        }
        if (rightTarget.IsTaunting)
        {
            PositionHypeCamera(rightTarget.transform);
        }
    }

    private void PositionHypeHitCamera(Transform target)
    {
        Camera.main.transform.position = defaultCameraPosition;
        Camera.main.transform.parent = null;
        Camera.main.orthographic = true;
        Camera.main.transform.LookAt(target.position);
        Camera.main.fieldOfView = (defaultZoom + zoomIn);
        //Time.timeScale = 1;
    }
    private void PositionHypeCamera(Transform target)
    {
        Camera.main.transform.LookAt(target);
        Camera.main.transform.SetParent(target);
        Camera.main.transform.position = center.transform.position;
        Camera.main.orthographic = false;
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, 45.50f, Camera.main.transform.position.z);
        Camera.main.fieldOfView = 45.0f;
    }
}

