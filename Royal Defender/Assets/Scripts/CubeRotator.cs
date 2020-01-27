using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotator : MonoBehaviour
{
    public int speed = 1;

    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * speed * Time.deltaTime);    
    }
}
