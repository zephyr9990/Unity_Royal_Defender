//Senior Desgin Project Points Manager
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{
    public static int points;

    Text text;

    void Awake()
    {
        text = GetComponent <Text> ();
        points = 0;
    }
    
    void Update()
    {
        text.text = points.ToString();
    }
}
