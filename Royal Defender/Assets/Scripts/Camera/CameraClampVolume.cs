using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClampVolume : MonoBehaviour
{
    public GameObject CameraToClamp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CameraToClamp.GetComponent<CameraFollow>().SetIsClamped(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CameraToClamp.GetComponent<CameraFollow>().SetIsClamped(false);
        }
    }
}
