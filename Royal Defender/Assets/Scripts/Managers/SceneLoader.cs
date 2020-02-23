using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public string nextSceneToLoad;
    public ScreenFader screenFader;
    public GameObject skipText;
    public float timeSkipTextVisible = 3f;
    public float timeToSkip = 3f;

    private bool skipTextShowing;
    private bool skipButtonIsHeldDown;
    private float timer;
    private float timeButtonHeldDown;
    private void Awake()
    {
        skipTextShowing = false;
        skipButtonIsHeldDown = false;
        timer = 0f;
        timeButtonHeldDown = 0f;
    }

    private void Start()
    {
        ShowSkipText();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButtonDown("SkipScene"))
        {
            ShowSkipText();
            SkipButtonHeld();
        }
        else if (Input.GetButtonUp("SkipScene"))
        {
            SkipButtonReleased();
        }

        if (skipButtonIsHeldDown)
        {
            timeButtonHeldDown += Time.deltaTime;
            if (timeButtonHeldDown > timeToSkip)
            {
                SkipScene();
            }
        }

        if (!skipButtonIsHeldDown 
            && skipTextShowing 
            && timer >= timeSkipTextVisible)
        {
            HideSkipText();
        }
    }
    
    private void SkipButtonHeld()
    {
        timeButtonHeldDown = Time.deltaTime;
        skipButtonIsHeldDown = true;
    }

    private void SkipButtonReleased()
    {
        timeButtonHeldDown = 0f;
        skipButtonIsHeldDown = false;
    }

    private void ShowSkipText()
    {
        skipTextShowing = true;
        skipText.SetActive(true);
    }

    private void HideSkipText()
    {
        timer = 0f;
        skipTextShowing = false;
        skipText.SetActive(false);
    }

    private void SkipScene()
    {
        screenFader.SkipScene();
        Invoke("LoadNextScene", 4f);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneToLoad);
    }

    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
