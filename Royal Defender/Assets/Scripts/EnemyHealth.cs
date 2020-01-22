using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int MaxHealth;

    private bool bIsAlive = true;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
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
    }

    public bool isAlive()
    {
        return bIsAlive;
    }

    private void Die()
    {
        bIsAlive = false;
        Destroy(gameObject, 1.0f);
    }
}
