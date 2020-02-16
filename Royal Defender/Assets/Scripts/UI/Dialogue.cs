using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public Text Speaker;
    public Text DialogueText;
    private DialogueTree dialogueTree;


    private bool needToDisplayText;
    private string textToDisplay;
    private string currentScene;
    private int currentLetterIndex;
    private float timer;

    private void Awake()
    {
        dialogueTree = GetComponent<DialogueTree>();
        needToDisplayText = false;
        textToDisplay = null;
        currentScene = "prologue";
        currentLetterIndex = 0;
        
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetButtonDown("ContinueText"))
        {
            if (needToDisplayText)
            {
                DisplayWholeDialogue();
            }
            else
            {
                ContinueDialogue();
            }
        }

        if(needToDisplayText && timer >= Time.deltaTime * 3f)
        {
            AddNextLetter(currentLetterIndex);
        }
    }

    public void ContinueDialogue()
    {
        DialogueText.text = "";
        dialogueTree.GenerateNextDialogue(currentScene);
    }

    public void ContinueDialogue(string scene)
    {
        currentScene = scene;
        dialogueTree.GenerateNextDialogue(scene);
    }

    public void DisplayOnUI(string speaker, string dialogue)
    {
        Speaker.text = speaker;
        textToDisplay = dialogue;
        currentLetterIndex = 0;
        needToDisplayText = true;
    }

    private void AddNextLetter(int letterIndex)
    {
        timer = 0f;
        if (DialogueText.text.Length == textToDisplay.Length)
        {
            ResetDialogueToDisplay();
            return;
        }

        DialogueText.text += textToDisplay[letterIndex];
        currentLetterIndex++;
    }

    private void DisplayWholeDialogue()
    {
        DialogueText.text = textToDisplay;
        ResetDialogueToDisplay();
    }

    private void ResetDialogueToDisplay()
    {
        needToDisplayText = false;
        textToDisplay = null;
    }
}