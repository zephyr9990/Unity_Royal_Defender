//Senior Desgin Project Points Manager
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesSlainManager : MonoBehaviour
{
    public static int enemiesSlain;

    public Text enemiesSlainText;

    void Awake()
    {
        enemiesSlainText = GetComponent<Text>();
        enemiesSlain = -1;
        CounterIncrease();
    }

    public void CounterIncrease()
    {
        enemiesSlain += 1;
        enemiesSlainText.text = "Enemies Slain\n" + enemiesSlain;
    }

    public int GetEnemiesSlain()
    {
        return enemiesSlain;
    }
}