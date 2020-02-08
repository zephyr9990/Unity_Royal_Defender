using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing;

    private Vector3 offset;
    private bool isClamped;
    private const float MIN_X_CLAMPED = -33.25274f;
    private const float MAX_X_CLAMPED = 37.08904f;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
        isClamped = false;
    }

    private void FixedUpdate()
    {
        Vector3 targetCameraPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCameraPosition, smoothing * Time.fixedDeltaTime);

        if (isClamped)
        {
            float xPos = Mathf.Clamp(transform.position.x, MIN_X_CLAMPED, MAX_X_CLAMPED);
            Vector3 clampedPosition = new Vector3(xPos, transform.position.y, transform.position.z);
            transform.position = clampedPosition;
        }
    }

    public void SetIsClamped(bool value)
    {
        isClamped = value;
    }
}
