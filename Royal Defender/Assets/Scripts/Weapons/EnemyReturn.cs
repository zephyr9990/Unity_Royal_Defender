using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReturn : MonoBehaviour
{
    public PlayerEquippedWeapon playerEquippedWeapon;
    public PauseManager pauseManager;
    private ArrayList enemies;
    private ArrayList originalTransforms;


    private void Awake()
    {
        enemies = new ArrayList();
    }

    public void SetEnemyOriginalPositions(ArrayList originalTransforms)
    {
        this.originalTransforms = originalTransforms;
    }

    public void ReturnEnemiesBackToOriginalPositions()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            TransformInfo originalTransform = (TransformInfo)originalTransforms[i];
            GameObject enemy = (GameObject)enemies[i];
            enemy.transform.position = originalTransform.position;

            //EnableMovement(enemy);
            playerEquippedWeapon.Shoot(enemy);
        }


        pauseManager.ResumeGameEnvironment();
        playerEquippedWeapon.DestroyWeapon();
        ClearEnemiesList();
    }

    private void EnableMovement(GameObject target)
    {
        if (target)
            target.GetComponent<IAIController>().EnableMovement();
    }

    private void ClearEnemiesList()
    {
        enemies.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject);
        }
    }
}
