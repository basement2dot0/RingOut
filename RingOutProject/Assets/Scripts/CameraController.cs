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

        CenterFocus();
    }
    
    void CenterFocus()
    {
        center.transform.position = (leftTarget.transform.position + rightTarget.transform.position) / 2;
        Camera.main.transform.LookAt(center.transform);

    }
}

