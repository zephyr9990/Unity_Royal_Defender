using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    public float lerpSmoothing = 7.0f;

    private bool bIsSprinting;
    private LockOnScript lockOn;
    private float sprintSpeed;
    private Vector3 movement;
    private CharacterController playerController;
    private Animator animator;
    private PlayerCombat playerCombat;
    private bool isLockedOn;

    private void Awake()
    {
        lockOn = GetComponentInChildren<LockOnScript>();
        playerCombat = GetComponent<PlayerCombat>();
        playerController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        sprintSpeed = speed + 3;
        isLockedOn = false;
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        bIsSprinting = Input.GetButton("Sprint");

        // Cannot move if swinging.
        bool PlayerIsSwinging = animator.GetBool("IsSwinging");
        if (PlayerIsSwinging)
            return;

        Move(horizontal, vertical);
    }

    private void Move(float horizontal, float vertical)
    {
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
        isLockedOn = lockOn.GetLockOnToggled();
        if (movement.magnitude != 0 && !isLockedOn)
        {
            Vector3 toTargetRotation = Vector3.RotateTowards(transform.forward, movement, Time.deltaTime * lerpSmoothing, 0.0f);
            transform.rotation = Quaternion.LookRotation(toTargetRotation);
        }
   }

    private Vector3 GetMovementSpeed()
    {
        if (bIsSprinting)
        {
            lockOn.TurnOffLockOn();
            playerCombat.StopAttackingAnimations();
            return movement.normalized * sprintSpeed * Time.fixedDeltaTime;
        }
        else if (isLockedOn)
        {
            return movement.normalized * (speed / 2) * Time.fixedDeltaTime;
        }
        else
        {
             return movement.normalized * speed * Time.fixedDeltaTime;
        }
    }
}
