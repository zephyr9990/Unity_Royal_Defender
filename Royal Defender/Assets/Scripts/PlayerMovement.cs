using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    public float lerpSmoothing = 7.0f;

    private bool bIsSprinting;
    private bool lockOnToggled;
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
        lockOnToggled = animator.GetBool("LockOnToggled");
        movement.Set(horizontal, 0.0f, vertical);

        animator.SetFloat("Speed", movement.magnitude);
        animator.SetFloat("VelocityX", transform.InverseTransformDirection(movement).x);
        animator.SetFloat("VelocityZ", transform.InverseTransformDirection(movement).z);
        animator.SetBool("IsSprinting", bIsSprinting);
        movement = GetMovementSpeed();

        playerController.Move(movement);

        // Gives player gravity acceleration
        playerController.Move(Physics.gravity);

        // Keeps rotation of last movement direction when not locked on
        if (movement.magnitude != 0 && !lockOnToggled)
        {
            Vector3 toTargetRotation = Vector3.RotateTowards(transform.forward, movement, Time.deltaTime * lerpSmoothing, 0.0f);
            transform.rotation = Quaternion.LookRotation(toTargetRotation);
        }
    }

    private Vector3 GetMovementSpeed()
    {
        if (bIsSprinting)
        {
             return movement.normalized * sprintSpeed * Time.fixedDeltaTime;
        }
        else if (lockOnToggled)
        {
            return movement.normalized * (speed / 2) * Time.fixedDeltaTime;
        }
        else
        {
             return movement.normalized * speed * Time.fixedDeltaTime;
        }
    }
}
