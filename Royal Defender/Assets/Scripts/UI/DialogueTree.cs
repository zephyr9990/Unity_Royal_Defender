using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTree : MonoBehaviour
{
    private ArrayList prologueSpeakerText;
    private ArrayList prologueSpeaker;
    private ArrayList continuationList;

    private int currentPrologueIndex;
    private Dialogue dialogue;

    private void Awake()
    {
        prologueSpeaker = new ArrayList();
        prologueSpeakerText = new ArrayList();
        continuationList = new ArrayList();
        currentPrologueIndex = 0;
        GeneratePrologueText();
        dialogue = GetComponent<Dialogue>();
    }

    public void GeneratePrologueText()
    {
        bool hasContinuation = true;
        bool hasNoContinuation = false;
        prologueSpeaker.Add("Aegis");
        prologueSpeakerText.Add("This is Paladin Aegis to HQ.");
        continuationList.Add(hasContinuation);
        
        prologueSpeaker.Add("HQ");
        prologueSpeakerText.Add("Reading you load and clear Aegis. This is \noperator Serah. Give us your report.");
        continuationList.Add(hasContinuation);

        prologueSpeaker.Add("Aegis");
        prologueSpeakerText.Add("I've discovered the cause of the \ndisturbance.");
        continuationList.Add(hasContinuation);

        prologueSpeaker.Add("Aegis");
        prologueSpeakerText.Add("The Chronocrystal has manifested itself \nwithin the Ruined City.");
        continuationList.Add(hasContinuation);

        prologueSpeaker.Add("Serah");
        prologueSpeakerText.Add("...!");
        continuationList.Add(hasContinuation);

        prologueSpeaker.Add("Serah");
        prologueSpeakerText.Add("With the power of the Chronocrystal, we \nmay yet have a chance to end this war.");
        continuationList.Add(hasContinuation);

        prologueSpeaker.Add("Serah");
        prologueSpeakerText.Add("We need you to bring it back to base.");
        continuationList.Add(hasContinuation);

        prologueSpeaker.Add("Aegis");
        prologueSpeakerText.Add("Acknowledged.");
        continuationList.Add(hasContinuation);

        prologueSpeaker.Add("Aegis");
        prologueSpeakerText.Add("Returning to base... after elimination of \nthe enemy squadron.");
        continuationList.Add(hasNoContinuation);

        prologueSpeaker.Add("Serah");
        prologueSpeakerText.Add("Stay safe, Aegis.");
        continuationList.Add(hasContinuation);

        prologueSpeaker.Add("Aegis");
        prologueSpeakerText.Add("Commencing mission.");
        continuationList.Add(hasNoContinuation);
    }

    public void GenerateNextDialogue(string scene)
    {
        if (scene.Equals("PROLOGUE", System.StringComparison.InvariantCultureIgnoreCase))
        {
            if (currentPrologueIndex < prologueSpeakerText.Count)
            {
                string currentSpeaker = (string)prologueSpeaker[currentPrologueIndex];
                string currentDialogue = (string)prologueSpeakerText[currentPrologueIndex];
                bool continueDialogue = (bool)continuationList[currentPrologueIndex];
                currentPrologueIndex++;

                dialogue.DisplayOnUI(currentSpeaker, currentDialogue, continueDialogue);
            }
        }
    }
}
