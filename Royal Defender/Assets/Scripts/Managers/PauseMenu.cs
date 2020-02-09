using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseMenu : MonoBehaviour
{
    public Transform pausedMenu;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
