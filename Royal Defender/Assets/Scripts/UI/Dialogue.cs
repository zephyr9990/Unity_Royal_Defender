using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class Dialogue : MonoBehaviour
{
    public float textSpeed = 2f;
    public Text speaker;
    public Text dialogueText;
    public GameObject[] timelines;

    private DialogueTree dialogueTree;
    private Animator animator;

    private bool textIsDisplayed;
    private bool needToDisplayText;
    private bool hasAdditionalDialogue;
    private string textToDisplay;
    private string currentScene;
    private int currentLetterIndex;
    private int currentTimelineIndex;
    private float timer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        dialogueTree = GetComponent<DialogueTree>();
        textIsDisplayed = false;
        needToDisplayText = false;
        hasAdditionalDialogue = false;  
        textToDisplay = null;
        currentScene = "prologue";
        currentLetterIndex = 0;
        currentTimelineIndex = 0;

        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (textIsDisplayed)
        {
            if (Input.GetButtonDown("ContinueText"))
            {
                if (needToDisplayText)
                {
                    DisplayWholeDialogue();
                }
                else if (hasAdditionalDialogue)
                {
                    ContinueDialogue();
                }
                else if (!hasAdditionalDialogue)
                {
                    EndDialogue();
                }
            }

            if (needToDisplayText && timer >= Time.deltaTime * textSpeed)
            {
                AddNextLetter(currentLetterIndex);
            }
        }
    }

    public void StartDialogueEvent()
    {
        animator.SetBool("ShowDialogue", true);
        ContinueDialogue(currentScene);
        textIsDisplayed = true;
    }

    private void EndDialogue()
    {
        animator.SetBool("ShowDialogue", false);
        textIsDisplayed = false;
        dialogueText.text = "";
        PlayNextTimeline();
    }

    private void ContinueDialogue()
    {
        dialogueText.text = "";
        dialogueTree.GenerateNextDialogue(currentScene);
    }

    private void ContinueDialogue(string scene)
    {
        currentScene = scene;
        dialogueTree.GenerateNextDialogue(scene);
    }

    private void PlayNextTimeline()
    {
        if (currentTimelineIndex < timelines.Length)
        {
            PlayableDirector cutscene = timelines[currentTimelineIndex].GetComponent<PlayableDirector>();
            cutscene.Play();

            currentTimelineIndex++;
        }
    }

    public void DisplayOnUI(DialogueInfo dialogue)
    {
        speaker.color = dialogue.speakerColor;
        speaker.text = dialogue.speaker;
        dialogueText.color = dialogue.textColor;
        textToDisplay = dialogue.text;
        hasAdditionalDialogue = dialogue.hasContinuation;

        currentLetterIndex = 0;
        needToDisplayText = true;
    }

    private void AddNextLetter(int letterIndex)
    {
        timer = 0f;
        if (dialogueText.text.Length == textToDisplay.Length)
        {
            ResetDialogueToDisplay();
            return;
        }

        dialogueText.text += textToDisplay[letterIndex];
        currentLetterIndex++;
    }

    private void DisplayWholeDialogue()
    {
        dialogueText.text = textToDisplay;
        ResetDialogueToDisplay();
    }

    private void ResetDialogueToDisplay()
    {
        needToDisplayText = false;
        textToDisplay = null;
    }
}