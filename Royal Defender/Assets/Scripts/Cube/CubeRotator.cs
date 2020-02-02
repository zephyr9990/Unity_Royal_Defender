using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotator : MonoBehaviour
{
    public int speed = 1;

    public float changeinX = 15;
    public float changeinY = 30;
    public float changeinZ = 45;

    void Update()
    {
        transform.Rotate(new Vector3(changeinX, changeinY, changeinZ) * speed * Time.deltaTime);
    }
}
