using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class CameraController : MonoBehaviour
{
    private void Update()
    {
       // Debug.Log(Camera.main.fieldOfView.ToString());
        // -------------------Code for Zooming Out------------
        if (Vector3.Distance(leftTarget.position,rightTarget.position) > 30 )
        {
            if (Camera.main.fieldOfView <= 100)
                Camera.main.fieldOfView += 2;
            if (Camera.main.orthographicSize <= 20)
                Camera.main.orthographicSize += 0f;

        }
        // ---------------Code for Zooming In------------------------
        if (Vector3.Distance(leftTarget.position, rightTarget.position) < 10)
        {
            if (Camera.main.fieldOfView > 50)
                Camera.main.fieldOfView -= 2;
            if (Camera.main.orthographicSize >= 1)
                Camera.main.orthographicSize -= 0.5f;
        }
    }



    [SerializeField]
    private Transform leftTarget;
    [SerializeField]
    public Transform rightTarget;

    //private FocusArea focusArea;
    //private Vector2 focusAreaSize;
    //public float boundsCenter;
    //private void Awake()
    //{
    //    focusArea = new FocusArea(leftTarget.GetComponent<CapsuleCollider>().bounds, focusAreaSize);

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    //float distanceBetweenTargets = Mathf.Abs(leftTarget.position.x - rightTarget.position.x) * 2;
    //    //float centerPosition = (leftTarget.position.x + rightTarget.position.x) / 2;

    //    //transform.position = new Vector3(
    //    //    centerPosition, transform.position.y,
    //    //    distanceBetweenTargets > minDistance ? -distanceBetweenTargets : -minDistance
    //    //    );
    //}

    //private void CameraZoom()
    //{
    //    Vector3 centerPosition = (leftTarget.position + rightTarget.position) / 2;

    //    if (Vector3.Distance(leftTarget.position,rightTarget.position) > 5)
    //    {
    //        transform.position += new Vector3(0, 0, 10)* 20 * Time.deltaTime;

    //    }
    //}
}

//struct FocusArea
//{
//    float left, top, right, bottom;
//    Vector3 velocity;
//    Vector2 center;

//    public FocusArea(Bounds bounds, Vector3 size)
//    {
//        left = bounds.center.x - size.x / 2.0f;
//        right = bounds.center.x + size.x / 2.0f;
//        top = bounds.center.y + size.y / 2.0f;
//        bottom = bounds.center.y - size.y / 2.0f;
//        velocity = Vector3.zero;
//        center = new Vector3((left + right) / 2.0f, (top + right) / 2.0f);
//    }
//}
