using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class enemyHealthBar : MonoBehaviour
{

    public Image foregroundImage;
    public Image backgroundImage;
    //Control the speed of update for the bar for a natural transition
    public float updateSpeedSeconds = 0.0f;


    EnemyHealth enemyHealth;

    public void Update()
    {
        if (enemyHealth.isLocked || enemyHealth.isDamaged)
        {
            DisplayHealth();
        }
        else if (!enemyHealth.isLocked || !enemyHealth.isDamaged) {
            TurnOffDisplay();
        }
    }

    //displayes the healthbar

    public void DisplayHealth()
    {
        foregroundImage.enabled = true;
        backgroundImage.enabled = true;
    }

    //Turn off healthbar display

    public void TurnOffDisplay()
    {
        foregroundImage.enabled = false;
        backgroundImage.enabled = false;
    }

    public void Awake()
    {
        enemyHealth = GetComponentInParent<EnemyHealth>();
        GetComponentInParent<EnemyHealth>().OnHealthPctChanged += HandleHealthChanged;
    }

    private void HandleHealthChanged(float pct)
    {
        StartCoroutine(ChangeToPct(pct));
    }

    private IEnumerator ChangeToPct(float pct)
    {
        /* float preChangePCT = foregroundImage.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePCT, pct, elapsed / updateSpeedSeconds);
            yield return null;
        } */

        foregroundImage.fillAmount = pct;
        yield return null;
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }

}
