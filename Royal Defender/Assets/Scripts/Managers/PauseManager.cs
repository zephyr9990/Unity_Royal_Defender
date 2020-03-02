using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject player;
    private ArrayList enemies;
    private PlayerMovement playerMovement;
    private PlayerInventory playerInventory;
    private BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        enemies = new ArrayList();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerInventory = player.GetComponent<PlayerInventory>();
        boxCollider.enabled = false;
    }

    private void Update()
    {
        if (boxCollider.enabled == true)
        {
            PauseGameEnvironment();
        }
    }

    private void PauseGameEnvironment()
    {
        DisableEnemyMovement();
        SetPlayerActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject);
        }
    }


    public void EnablePausingVolume()
    {
        boxCollider.enabled = true;
    }

    private void DisableEnemyMovement()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            GameObject enemy = (GameObject)enemies[i];
            if (enemy)
            {
                enemy.GetComponent<IAIController>().StopMovement();
            }
        }
    }

    private void EnableEnemyMovement()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            GameObject enemy = (GameObject)enemies[i];
            if (enemy)
            {
                enemy.GetComponent<IAIController>().EnableMovement();
            }
        }
    }

    public void ResumeGameEnvironment()
    {
        EnableEnemyMovement();
        SetPlayerActive(true);
        boxCollider.enabled = false;
        enemies.Clear();
    }

    private void SetPlayerActive(bool value)
    {
        if (player)
        {
            playerInventory.EnableInventoryControl(value);
            playerMovement.enabled = value;
        }
    }
}
