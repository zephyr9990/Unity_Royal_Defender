using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyHealth : MonoBehaviour
{
    public int MaxHealth;
    public int scoreValue = 10;
    private GameObject player;
    private LockOnScript playerLockOnScript;
    private Animator _anim;
    private bool bIsAlive = true;
    private int currentHealth;
    private NavMeshAgent _nav;

    public bool isLocked = false;
    public bool isDamaged = false;
    public int displayTime = 3;




    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        _anim = GetComponent<Animator>();
        _nav = GetComponent<NavMeshAgent>();
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
                isLocked = false;
                isDamaged = false;
                Die();
            }
            else
            {
                if (!isLocked)
                {
                    isDamaged = true;
                    StartCoroutine(TimedHealthBar(displayTime));
                }
                currentHealth -= amount;

                //Update the health slider;
                UpdateHealthPCT();
            }
            Debug.LogWarning(gameObject.name + " HP: " + currentHealth);

        }
    }

    IEnumerator TimedHealthBar(int time)
    {
        yield return new WaitForSeconds(time);
        isDamaged = false;
    }

    public bool isAlive()
    {
        return bIsAlive;
    }

    private void Die()
    {
        _anim.SetTrigger("Die");
        _nav.isStopped = true;

        bIsAlive = false;
        ScoreManager.score += scoreValue;
        Destroy(gameObject, 2.5f);
    }
}
