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

    public float minDistance;

    // Update is called once per frame
    void Update()
    {
        float distanceBetweenTargets = Mathf.Abs(leftTarget.position.x - rightTarget.position.x) * 2;
        float centerPosition = (leftTarget.position.x + rightTarget.position.x) / 2;

        transform.position = new Vector3(
            centerPosition, transform.position.y,
            distanceBetweenTargets > minDistance ? -distanceBetweenTargets : -minDistance
            );
    }
}

