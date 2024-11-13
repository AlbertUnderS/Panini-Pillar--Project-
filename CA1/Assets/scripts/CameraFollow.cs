using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // The target object for the camera to follow
    public Transform target;

    // The distance between the camera and the target
    public Vector3 offset = new Vector3(0, 5, -10);

    // The smooth speed of the camera’s movement
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired position
            Vector3 desiredPosition = target.position + offset;

            // Smoothly move the camera towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Update the camera’s position
            transform.position = smoothedPosition;

            // Optional: Look at the target
            transform.LookAt(target);
        }
    }
}
