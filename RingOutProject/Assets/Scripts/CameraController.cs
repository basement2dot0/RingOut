using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform leftTarget;

    [SerializeField]
    private Transform rightTarget;

    [SerializeField]
    private GameObject center;

    private void Update()
    {

        CenterFocus(this.center);
    }
    
    /// <summary>
    /// Keeps the camera constantly focused on the Center GameObject
    /// </summary>
    private void CenterFocus(GameObject center)
    {
        center = this.center;
        center.transform.position = (leftTarget.transform.position + rightTarget.transform.position) / 2;
        Camera.main.transform.LookAt(center.transform);
    }
}

