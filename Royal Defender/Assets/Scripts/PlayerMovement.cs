﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    private bool bIsSprinting;
    private float sprintSpeed;
    private Vector3 movement;
    private CharacterController playerController;
    private Animator animator;

    private void Awake()
    {
        playerController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        sprintSpeed = speed + 3;
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        bIsSprinting = Input.GetButton("Sprint");
        Move(horizontal, vertical);
    }

    private void Move(float horizontal, float vertical)
    {
        movement.Set(horizontal, 0.0f, vertical);

        animator.SetFloat("Speed", movement.magnitude);
        animator.SetBool("IsSprinting", bIsSprinting);

        if (bIsSprinting)
        {
            movement = movement.normalized * sprintSpeed * Time.fixedDeltaTime;
        }
        else
        {
            movement = movement.normalized * speed * Time.fixedDeltaTime;
        }

        playerController.Move(movement);

        // Gives player gravity acceleration
        playerController.Move(Physics.gravity);

        // Keeps rotation of last movement direction
        if (movement.magnitude != 0)
        {
            Quaternion newRotation = Quaternion.LookRotation(movement);
            transform.rotation = newRotation;
        }
    }
}
