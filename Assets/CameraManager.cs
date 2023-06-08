using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    public Transform cameraPivot;
    public float cameraFollowSpeed = 0.2f;

    public float lookAngle;
    public float pivotAngle;
    public float camaraLookSpeed = 0.2f;
    public float camaraPivotSpeed = 0.2f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        
        this.FollowTarget();
        this.RotateCamera();
    }

    private void FollowTarget()
    {
        Vector3 targetPos = Vector3.SmoothDamp(transform.position, target.position, ref cameraFollowVelocity, cameraFollowSpeed);
        
        transform.position = targetPos;
    }

    private void RotateCamera()
    {
        lookAngle = lookAngle + Input.GetAxis("Mouse X") * camaraLookSpeed;
        pivotAngle = pivotAngle - Input.GetAxis("Mouse Y") * camaraPivotSpeed;
        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        rotation.x = pivotAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }
}
