using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour

{


    public bool isStart;
    public bool isQuit;


    void OnMouseUp()
    {
        if (isStart)
        {
            SceneManager.LoadScene("PrologueScene", LoadSceneMode.Single);
            SceneManager.UnloadScene("MainMenu");
        }
        if (isQuit)
        {
            Application.Quit();
        }
    }
}
