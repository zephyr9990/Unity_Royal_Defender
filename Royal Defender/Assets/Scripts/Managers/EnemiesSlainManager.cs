//Senior Desgin Project Points Manager
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesSlainManager : MonoBehaviour
{
    public static int enemiesslain;

    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
        enemiesslain = 0;
    }

    void Update()
    {
        text.text = "Enemies Slain: " + enemiesslain;
    }
}