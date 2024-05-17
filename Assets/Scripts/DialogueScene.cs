using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueScene
{
    public List<Dialogue> dialogueLines;

    public DialogueScene()
    {
        dialogueLines = new List<Dialogue>();
    }
}

[System.Serializable]
public class Dialogue
{
    public string speakerName;
    [TextArea(2, 5)]
    public List<string> speakerLines;
    public SpeakerPosition speakerPosition;

    public Dialogue()
    {
        speakerLines = new List<string>();
    }
}

[System.Serializable]
public enum SpeakerPosition
{
    Left = 0,
    Middle = 1,
    Right = 2
}