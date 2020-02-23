//Senior Desgin Project Points Manager
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    float startTime = 310f; // 5:10 minute ten seconds
    float timeRemaining;

    public Text timerText;
    int enemiesSlain;

    public float killIncrease = 15f; // 15 second default time for 15 enemies
    public float stoneIncrease = 60f; //60 seocnd default increase for stone
    public bool killsBeenUpdated = true;
    public int enemiesKilledCounter = 5;
    EnemiesSlainManager enemiesSlainScript;

    PlayerHealth playerHealth;

    void Start()
    {
        timerText = GetComponent<Text>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        enemiesSlainScript = GameObject.FindGameObjectWithTag("SlainText").GetComponent<EnemiesSlainManager>();
        timeRemaining = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        enemiesSlain = enemiesSlainScript.GetEnemiesSlain();
        timeRemaining -= Time.deltaTime;
        if (!killsBeenUpdated && (enemiesSlain % enemiesKilledCounter == 0))
        {
            IncreaseTimer(killIncrease);
            killsBeenUpdated = true;
        }
        else if (killsBeenUpdated && !(enemiesSlain % enemiesKilledCounter == 0))
        {
            killsBeenUpdated = false;
        }
        UpdateLevelTimer(timeRemaining);
    }

    public void UpdateLevelTimer(float totalSeconds)
    {
        if (totalSeconds <= 0.0f)
        {
            totalSeconds = 0;
            playerHealth.Death();
        }

            int minutes = Mathf.FloorToInt(totalSeconds / 60f);
            int seconds = Mathf.RoundToInt(totalSeconds % 60f);

            string formatedSeconds = seconds.ToString();

            if (seconds == 60)
            {
                seconds = 0;
                minutes += 1;
            }

            timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        
    }

    public void IncreaseTimer(float increaseTime)
    {
        timeRemaining += killIncrease;
    }


}
