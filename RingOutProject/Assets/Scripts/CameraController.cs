//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using UnityEngine;


//class CameraController : MonoBehaviour
//{
//    [SerializeField]
//    private PlayerOne leftTarget;

//    [SerializeField]
//    private float fov;

//    [SerializeField]
//    private PlayerTwo rightTarget;

//    [SerializeField]
//    private GameObject center;

//    [SerializeField]
//    private float zoomIn;
//    [SerializeField]
//    private float defaultZoom;

//    private float lastFrameTime;
//    private float myDeltaTime;

//    private void Awake()
//    {
//        leftTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerOne>();
//        rightTarget = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<PlayerTwo>();
//        defaultZoom = Camera.main.fieldOfView;
//        lastFrameTime = Time.realtimeSinceStartup;
//        fov = 10;
//        zoomIn = 50.0f;
//    }
//    private void Update()
//    {
//        Debug.Log(Camera.main.fieldOfView.ToString());
//        CenterFocus();
        
//    }

//    private void LateUpdate()
//    {
//        myDeltaTime = Time.realtimeSinceStartup - lastFrameTime;
//        lastFrameTime = Time.realtimeSinceStartup;
//    }

//    /// <summary>
//    /// Keeps the camera constantly focused on the Center GameObject
//    /// </summary>
//    private void CenterFocus()
//    {
//        if (leftTarget.currentState == State.Hit)
//        {
//            center.transform.position = leftTarget.transform.position;
//            Camera.main.transform.LookAt(center.transform);
//            Camera.main.fieldOfView = (defaultZoom + zoomIn); 

//        }
//        if (rightTarget.currentState == State.Hit)
//        {
//            center.transform.position = rightTarget.transform.position;
//            Camera.main.transform.LookAt(center.transform);
//            if(Camera.main.fieldOfView >= fov)
//                Camera.main.fieldOfView = fov;
//            Time.timeScale = 1;
            
            
            
//        }
//        else
//        {
//            center.transform.position = (leftTarget.transform.position + rightTarget.transform.position) / 2;
//            Camera.main.transform.LookAt(center.transform);
//        }
//    }
//}

