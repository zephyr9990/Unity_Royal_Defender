using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToGame()
    {
        SceneManager.LoadScene("PrologueScene", LoadSceneMode.Single);
    }

    public void SkipPrologue()
    {
        SceneManager.LoadScene("CastleTownScene", LoadSceneMode.Single);
    }

    public void Credits()

    {
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }

    public void Quit()

    {
        Application.Quit();
    }

}