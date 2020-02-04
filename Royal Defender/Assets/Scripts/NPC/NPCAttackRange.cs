using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAttackRange : MonoBehaviour
{
    private ArrayList enemies;

    private void Awake()
    {
        enemies = new ArrayList();
    }

    public ArrayList getEnemiesInAttackRange()
    {
        return enemies;
    }

    public GameObject GetNearestTarget()
    {
        GameObject target = null;
        if (enemies.Count > 0)
        {
            Vector3 distanceToClosestEnemy = Vector3.zero;
            for (int index = 0; index < enemies.Count; index++)
            {
                GameObject enemy = enemies[index] as GameObject;
                if (enemy)
                {
                    EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                    if (enemyHealth.isAlive())
                    {
                        Vector3 distanceToEnemy = enemy.transform.position - transform.position;
                        if (distanceToClosestEnemy == Vector3.zero)
                        {
                            distanceToClosestEnemy = distanceToEnemy;
                            target = enemy;
                        }
                        else if (distanceToEnemy.magnitude < distanceToClosestEnemy.magnitude)
                        {
                            distanceToClosestEnemy = distanceToEnemy;
                            target = enemy;
                        }
                    }
                    else
                    {
                        enemies.Remove(enemy);
                    }
                }
            }
        }

        return target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Remove(other.gameObject);
        }
    }
}
