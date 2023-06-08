using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float rotateSpeed = 15f;


    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private Transform cameraObject;
    [SerializeField] private Rigidbody playerRigid;
    // Start is called before the first frame update
    private void Awake()
    {
        this.cameraObject = Camera.main.transform;
        this.playerRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        this.Move();    
    }

    private void HandleMovement()
    {
        this.moveDirection = cameraObject.forward * Input.GetAxis("Vertical");
        this.moveDirection += cameraObject.right * Input.GetAxis("Horizontal");
        moveDirection.Normalize();
        moveDirection.y = 0;

        Vector3 moveVelocity = moveDirection * moveSpeed;
        playerRigid.velocity = moveVelocity;
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * Input.GetAxis("Vertical");
        targetDirection += cameraObject.right * Input.GetAxis("Horizontal");
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        playerRigid.MoveRotation(playerRotation);
    }

    private void Move()
    {
        HandleMovement();
        HandleRotation();

        if (moveDirection != Vector3.zero)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }
            else
            {
                Walk();
            }
        }
        else
        {
            Idle();
        }
    }

    private void Idle()
    {
        this.moveSpeed = 0f;
        this.animator.SetFloat("Speed", 0);
    }

    private void Walk()
    {
        this.moveSpeed = walkSpeed;
        this.animator.SetFloat("Speed", 0.3f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        this.moveSpeed = runSpeed;
        this.animator.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);
    }
}
