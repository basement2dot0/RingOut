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
    public Transform rightTarget;
    [SerializeField]
    private BoxCollider StageCollider;
    [SerializeField]
    [Range(0, 100)]
    private float fovOut;
    [SerializeField]
    [Range(0, 100)]
    private float fovIn;

    private void Start()
    {
        StageCollider = GameObject.FindGameObjectWithTag("StageBounds").GetComponent<BoxCollider>();
    }

    private void Update()
    {
        ZoomIn();
        ZoomOut();
    }

    private void ZoomIn()
    {
        //Debug.Log("Player One:" + leftTarget.position.ToString() + ", StageColliderMin:" + StageCollider.bounds.min.ToString());
        if(leftTarget.position.z > 0.0f)
        {
            if (Camera.main.fieldOfView > 20)
                Camera.main.fieldOfView -= 2;
        }
    }

    private void ZoomOut()
    {
        if(leftTarget.position.z < 0.0f)
        {
            if (Camera.main.fieldOfView <= 70)
                Camera.main.fieldOfView += 2;
        }
    }
}