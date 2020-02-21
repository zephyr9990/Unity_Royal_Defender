using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DialogueInfo
{
    public Color speakerColor;
    public string speaker;
    public Color textColor;
    public string text;
    public bool hasContinuation;

    public DialogueInfo(Color speakerColor, string speaker, Color textColor, string text, bool hasContinuation)
    {
        this.speakerColor = speakerColor;
        this.speaker = speaker;
        this.textColor = textColor;
        this.text = text;
        this.hasContinuation = hasContinuation;
    }
}
