    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Introduction : MonoBehaviour
{
    public Text title;
    public Text name;
    private Animator animator;

    private void Awake()
    {
        title.text = "";
        name.text = "";
        animator = GetComponent<Animator>();
    }

    public void SetTitle(string title)
    {
        this.title.text = title;
    }

    public void SetName(string name)
    {
        this.name.text = name;
    }

    public void PlayIntroduction()
    {
        animator.SetTrigger("PlayIntro");
    }

    public void IntroFinishedEvent()
    {
        animator.ResetTrigger("PlayIntro");
    }

}
