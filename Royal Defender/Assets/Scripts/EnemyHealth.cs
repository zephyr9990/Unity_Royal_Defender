using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int MaxHealth;
    public int scoreValue = 10;
    Animator _anim;
    private bool bIsAlive = true;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth - amount <= 0)
        {
            currentHealth = 0;
            Die();
        }
        else
        {
            currentHealth -= amount;
        }
        Debug.LogWarning(currentHealth);
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
