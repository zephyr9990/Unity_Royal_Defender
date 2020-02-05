using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    public int MaxHealth;
    public int scoreValue = 10;
    private GameObject player;
    private LockOnScript playerLockOnScript;
    Animator _anim;
    private bool bIsAlive = true;
    private int currentHealth;

    public bool isLocked = false;
    public bool isDamaged = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        _anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerLockOnScript = player.transform.GetChild(0).GetComponent<LockOnScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public event Action<float> OnHealthPctChanged = delegate { };

    public void UpdateHealthPCT()
    {
        float currentHealthPct = (float)currentHealth / (float)MaxHealth;
        OnHealthPctChanged(currentHealthPct);
    }

    public void TakeDamage(int amount)
    {
        if (bIsAlive)
        {
            if (currentHealth - amount <= 0)
            {
                currentHealth = 0;
                Die();
            }
            else
            {
                currentHealth -= amount;

                //Update the percent slider when locked on
                if (isLocked)
                {
                    UpdateHealthPCT();
                }

            }
            Debug.LogWarning(gameObject.name + " HP: " + currentHealth);
        }
    }

    public bool isAlive()
    {
        return bIsAlive;
    }

    private void Die()
    {
        _anim.SetTrigger("Die");

        bIsAlive = false;
        ScoreManager.score += scoreValue;
        Destroy(gameObject, 2.0f);
    }
}
