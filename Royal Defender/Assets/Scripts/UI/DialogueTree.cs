using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTree : MonoBehaviour
{
    private ArrayList dialogueInfos;

    private int currentPrologueIndex;
    private Dialogue dialogue;

    private void Awake()
    {
        dialogueInfos = new ArrayList();
        currentPrologueIndex = 0;
        GeneratePrologueText();
        dialogue = GetComponent<Dialogue>();
    }

    public void GeneratePrologueText()
    {
        Color aegisColor = new Color(0f, .5f, 1f);
        aegisColor.a = 0f;
        Color serahColor = Color.cyan;
        serahColor.a = 0f;
        Color narratorColor = Color.green;
        narratorColor.a = 0f;
        Color defaultTextColor = Color.white;
        Color narratorTextColor = Color.green;

        string aegisName = "Aegis";
        string operatorName = "Serah";
        string narratorName = "Narrator";

        bool hasContinuation = true;
        bool hasNoContinuation = false;

        DialogueInfo dialogueInfo = new DialogueInfo(
            aegisColor,
            aegisName,
            defaultTextColor,
            "This is Paladin Aegis to HQ. Do you copy?",
            hasContinuation
            );
        AddDialogue(dialogueInfo);

        dialogueInfo = new DialogueInfo(
            serahColor,
            operatorName,
            defaultTextColor,
            "Reading you loud and clear, Aegis. This is \noperator Serah. Give us your report.",
            hasContinuation
            );
        AddDialogue(dialogueInfo);

        dialogueInfo = new DialogueInfo(
            aegisColor,
            aegisName,
            defaultTextColor,
            "I've discovered the cause of the \ndisturbance.",
            hasContinuation
            );
        AddDialogue(dialogueInfo);

        dialogueInfo = new DialogueInfo(
            aegisColor,
            aegisName,
            defaultTextColor,
            "The Chrono Crystal has manifested itself \nwithin the heart of the Ruined City.",
            hasContinuation);
        AddDialogue(dialogueInfo);

        dialogueInfo = new DialogueInfo(
            serahColor,
            operatorName,
            defaultTextColor,
            "...!",
            hasContinuation);
        AddDialogue(dialogueInfo);

        dialogueInfo = new DialogueInfo(
            serahColor,
            operatorName,
            defaultTextColor,
            "We may yet have a chance to end this war.",
            hasContinuation);
        AddDialogue(dialogueInfo);

        dialogueInfo = new DialogueInfo(
            serahColor,
            operatorName,
            defaultTextColor,
            "With the discovery of the Chrono crystal, \nthe commander has issued a new directive for designation Aegis.",
            hasContinuation);
        AddDialogue(dialogueInfo);

        dialogueInfo = new DialogueInfo(
            aegisColor,
            aegisName,
            defaultTextColor,
            "I'm listening.",
            hasContinuation);
        AddDialogue(dialogueInfo);

        dialogueInfo = new DialogueInfo(
            serahColor,
            operatorName,
            defaultTextColor,
            "\"Paladin, designation Aegis, is to extract \nthe Chrono Crystal from the heart of the \nRuined City and return to base.\"",
            hasContinuation);
        AddDialogue(dialogueInfo);

        dialogueInfo = new DialogueInfo(
            aegisColor,
            aegisName,
            defaultTextColor,
            "Acknowledged.",
            hasContinuation); 
        AddDialogue(dialogueInfo);

        dialogueInfo = new DialogueInfo(
            aegisColor,
            aegisName,
            defaultTextColor,
            "Returning to base...",
            hasContinuation
            );
        AddDialogue(dialogueInfo);

        dialogueInfo = new DialogueInfo(
            aegisColor,
            aegisName,
            defaultTextColor,
            "After neutralization of enemy hostiles.",
            hasNoContinuation);
        AddDialogue(dialogueInfo);

        dialogueInfo = new DialogueInfo(
            narratorColor,
            narratorName,
            narratorTextColor,
            "Aegis engaged in combat against his \nassailants.",
            hasContinuation);
        AddDialogue(dialogueInfo);

        dialogueInfo = new DialogueInfo(
            narratorColor,
            narratorName,
            narratorTextColor,
            "With quick reflexes and precise aim, Aegis deftly dispatched foe after foe.",
            hasContinuation);
        AddDialogue(dialogueInfo);

        dialogueInfo = new DialogueInfo(
            narratorColor,
            narratorName,
            narratorTextColor,
            "...but Aegis' assailants, the military group known as the Dark Knights, were \npersistent.",
            hasContinuation);
        AddDialogue(dialogueInfo);

        dialogueInfo = new DialogueInfo(
            narratorColor,
            narratorName,
            narratorTextColor,
            "Knowing full well the potential the \nChrono crystal possesses to change the \noutcome of the war, " +
            "the Dark Knights",
            hasContinuation);
        AddDialogue(dialogueInfo);

        dialogueInfo = new DialogueInfo(
            narratorColor,
            narratorName,
            narratorTextColor,
            "chose to deploy their trump card.",
            hasContinuation);
        AddDialogue(dialogueInfo);

        dialogueInfo = new DialogueInfo(
            narratorColor,
            narratorName,
            narratorTextColor,
            "Aegis was blindsided....",
            hasNoContinuation);
        AddDialogue(dialogueInfo);
    }

    public void GenerateNextDialogue(string scene)
    {
        if (scene.Equals("PROLOGUE", System.StringComparison.InvariantCultureIgnoreCase))
        {
            if (currentPrologueIndex < dialogueInfos.Count)
            {
                DialogueInfo speakerDialogue = (DialogueInfo)dialogueInfos[currentPrologueIndex];
                currentPrologueIndex++;

                dialogue.DisplayOnUI(speakerDialogue);
            }
        }
    }

    private void AddDialogue(DialogueInfo dialogueInfo)
    {
        dialogueInfos.Add(dialogueInfo);
    }
}
