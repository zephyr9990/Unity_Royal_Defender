using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTree : MonoBehaviour
{
    private ArrayList prologueSpeakerText;
    private ArrayList prologueSpeaker;
    private int currentPrologueIndex;
    private Dialogue dialogue;

    private void Awake()
    {
        prologueSpeaker = new ArrayList();
        prologueSpeakerText = new ArrayList();
        currentPrologueIndex = 0;
        GeneratePrologueText();
        dialogue = GetComponent<Dialogue>();
    }

    public void GeneratePrologueText()
    {
        prologueSpeaker.Add("Aegis");
        prologueSpeakerText.Add("This is Paladin Aegis to HQ.");
        
        prologueSpeaker.Add("HQ");
        prologueSpeakerText.Add("Reading you load and clear Aegis. This is operator Serah. Give us your report.");

        prologueSpeaker.Add("Aegis");
        prologueSpeakerText.Add("I've discovered the cause of the disturbance.");

        prologueSpeaker.Add("Aegis");
        prologueSpeakerText.Add("The Chronocrystal has manifested itself within the Ruined City.");

        prologueSpeaker.Add("Serah");
        prologueSpeakerText.Add("...!");

        prologueSpeaker.Add("Serah");
        prologueSpeakerText.Add("With the power of the Chronocrystal, we may yet have a chance to end this long and grueling war.");

        prologueSpeaker.Add("Serah");
        prologueSpeakerText.Add("We need you bring it back to base.");

        prologueSpeaker.Add("Aegis");
        prologueSpeakerText.Add("Acknowledged.");

        prologueSpeaker.Add("Aegis");
        prologueSpeakerText.Add("Returning to base... after elimination of the enemy squadron.");

        prologueSpeaker.Add("Serah");
        prologueSpeakerText.Add("Stay safe, Aegis.");

        prologueSpeaker.Add("Aegis");
        prologueSpeakerText.Add("Commencing mission.");
    }

    public void GenerateNextDialogue(string scene)
    {
        if (scene.Equals("PROLOGUE", System.StringComparison.InvariantCultureIgnoreCase))
        {
            string currentSpeaker = (string)prologueSpeaker[currentPrologueIndex];
            string currentDialogue = (string)prologueSpeakerText[currentPrologueIndex];
            currentPrologueIndex++;

            dialogue.DisplayOnUI(currentSpeaker, currentDialogue);
        }
    }
}
