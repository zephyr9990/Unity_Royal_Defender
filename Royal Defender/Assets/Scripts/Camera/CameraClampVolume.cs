using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClampVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Camera.main.GetComponent<CameraFollow>().SetIsClamped(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Camera.main.GetComponent<CameraFollow>().SetIsClamped(false);
        }
    }
}
