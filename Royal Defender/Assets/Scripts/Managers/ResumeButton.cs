using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour
{
    public Button resume;
    void Start()
    {
        Button btn = resume.GetComponent<Button>();
        btn.onClick.AddListener(Clicked);
    }

    void Update()
    {
        
    }

    public void Clicked()
    {
        FindObjectOfType<PauseMenu>().Paused();
    }
}
