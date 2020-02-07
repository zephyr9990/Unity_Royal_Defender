using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    // Start is called before the first frame update
    public int secondsBeforeDestroyed = 20;

    void Start()
    {
        Destroy(gameObject, secondsBeforeDestroyed);
    }
}
