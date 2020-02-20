using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseMenu : MonoBehaviour
{
    public Transform pausedMenu;
    public GameObject player;
    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause")
            && playerHealth.isGreaterThanZero())
        {
            Paused();
        }
    }

    public void Paused()
    {
        if (pausedMenu.gameObject.activeInHierarchy == false)
        {
            pausedMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
            AudioListener.pause = true;
        }
        else
        {
            pausedMenu.gameObject.SetActive(false);
            Time.timeScale = 1;
            AudioListener.pause = false;
        }
    }
}
