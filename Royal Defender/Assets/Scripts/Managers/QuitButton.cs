using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    public Button quitGame;
    void Start()
    {
        Button btn = quitGame.GetComponent<Button>();
        btn.onClick.AddListener(Exit);
    }

    void Update()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }
}

