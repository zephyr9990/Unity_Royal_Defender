using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    private Vector3 movement;
    private Rigidbody playerRigidbody;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Move(horizontal, vertical);
    }

    private void Move(float horizontal, float vertical)
    {
        movement.Set(horizontal, 0.0f, vertical);
        movement = movement.normalized * speed * Time.fixedDeltaTime;

        playerRigidbody.MovePosition(transform.position + movement);

        // Keeps rotation of last movement direction
        if (movement.magnitude != 0)
        {
            Quaternion newRotation = Quaternion.LookRotation(movement);
            transform.rotation = newRotation;
        }
    }
}
