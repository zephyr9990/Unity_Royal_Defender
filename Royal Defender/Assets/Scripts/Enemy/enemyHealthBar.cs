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
    public float updateSpeedSeconds = 0.5f;
    public bool locked;

    EnemyHealth enemyHealth;

    public void Update()
    {
        locked = enemyHealth.isLocked;
        if (locked && foregroundImage.enabled == false)
        {
            foregroundImage.enabled = true;
            backgroundImage.enabled = true;
        }
        else if (!locked && foregroundImage.enabled == true) {
            foregroundImage.enabled = false;
            backgroundImage.enabled = false;
        }
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
        float preChangePCT = foregroundImage.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePCT, pct, elapsed / updateSpeedSeconds);
            yield return null;
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }

}
