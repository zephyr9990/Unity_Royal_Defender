//Senior Desgin Project Points Manager
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesSlainManager : MonoBehaviour
{
    public int enemiesSlain;
    private TimerManager timeManagerScript;
    public Text enemiesSlainText;
    public int enemiesSlainCounter = 5;

    void Awake()
    {
        enemiesSlainText = GetComponent<Text>();
        timeManagerScript = GameObject.FindGameObjectWithTag("TimerText").GetComponent<TimerManager>();
        enemiesSlain = 0;
        UpdateTextField();
    }

    public void CounterIncrease()
    {
        enemiesSlain += 1;
        UpdateTextField();
    }

    private void UpdateTextField()
    {
        enemiesSlainText.text = "Enemies Slain\n" + enemiesSlain;
    }

    public int GetEnemiesSlain()
    {
        return enemiesSlain;
    }
}