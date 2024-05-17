using System.Collections;
using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
    [SerializeField] DialogueNode startingNode;


    public UnityEvent OnDialogueStart, OnDialogueEnd;

    DialogueNode currentNode;
    DialogueScene currentScene;

    [SerializeField] int currentSpeakerLine;
    [SerializeField] int currentDialogueLine;

    [SerializeField] float typeSpeed = 0.2f;

    [SerializeField] TextMeshProUGUI dialogueText, nameText;
    [SerializeField] Image textBox, nameBox;


    public CharacterManager characterHandler;
    public List<Character> characters;

    private Character characterSpeaking;

    [SerializeField] MonoTweener rightIdleTween, rightTalkingTween;
    [SerializeField] MonoTweener leftIdleTween, leftTalkingTween;

    [Space]
    [Header("Dialogue Runner Bools")]
    bool isTyping = false;
    bool cancelTyping = false;
    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        characterHandler = FindObjectOfType<CharacterManager>();
        characters = characterHandler.characterList;
        if(startingNode != null)
        {
            StartDialogueNode(startingNode);
            isActive = true;
        }

        if(rightIdleTween)
        {
            rightIdleTween.Play();
        }

        if(leftIdleTween)
        {
            leftIdleTween.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextLine();
        }
    }


    public void StartDialogueNode(DialogueNode node)
    {
        currentDialogueLine = 0;
        currentSpeakerLine = 0;
        currentNode = node; 
        currentScene = node.scene;
        if (!isActive)
        {
            OnDialogueStart?.Invoke();
        }
        StartTyping();

    }

    public void StartDialogueNode(DialogueNode node, int dialogueLine)
    {
        currentSpeakerLine = 0;
        currentNode = node;
        currentScene = node.scene;
        currentDialogueLine = dialogueLine;
        if (!isActive)
        {
            OnDialogueStart?.Invoke();
        }
        StartTyping();

    }

    public void ShowNextLine() // shows the next line of dialogue
    {
        KillTalkingTweener();

        if (isActive)
        {
            if (!isTyping) // if no text is typing go to next line OR disable textbox
            {
                currentSpeakerLine++;
                if (currentSpeakerLine >= currentScene.dialogueLines[currentDialogueLine].speakerLines.Count)
                {
                    if (currentDialogueLine < currentScene.dialogueLines.Count - 1)
                    {
                        currentDialogueLine++;
                        currentSpeakerLine = 0;
                        StartTyping();
                    }
                    else
                    {
                        if (currentNode.nextNode != null)
                        {
                            StartDialogueNode(currentNode.nextNode);
                        }
                        else
                        {
                            OnDialogueEnd?.Invoke();
                            Debug.Log("No more dialogue to show!");
                        }
                    }
                }
                else
                {
                    StartTyping();
                }
            }
            else if (isTyping && !cancelTyping) // cancel typing!
            {
                cancelTyping = true;
                dialogueText.text = currentScene.dialogueLines[currentDialogueLine].speakerLines[currentSpeakerLine];
            }

        }
    }

    public void StartTyping()
    {
        string speakerName = currentScene.dialogueLines[currentDialogueLine].speakerName;
        string lineToShow = currentScene.dialogueLines[currentDialogueLine].speakerLines[currentSpeakerLine];

        characterSpeaking = GetCharacterFromSpeakerName(speakerName);
        nameBox.color = characterSpeaking.Color;

        nameText.text = characterSpeaking.Name;
        StartCoroutine(TypeOutText(lineToShow));
        isActive = true;
        if (currentScene.dialogueLines[currentDialogueLine].speakerPosition == 0)
        {
            leftTalkingTween.Play();
        }
        else
        {
            rightTalkingTween.Play();
        }
    }

    IEnumerator TypeOutText(string text)
    {
        dialogueText.text = "";
        int letter = 0;


        isTyping = true;
        cancelTyping = false;

        while (isTyping && !cancelTyping && (letter < text.Length))
        {
            dialogueText.text += text[letter];
            letter += 1;
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;
        cancelTyping = false;
        KillTalkingTweener();

    }

    public void KillTalkingTweener()
    {
        if (currentScene.dialogueLines[currentDialogueLine].speakerPosition == 0)
        {
            leftTalkingTween.Kill();
        }
        else
        {
            rightTalkingTween.Kill();
        }
    }

    public Character GetCharacterFromSpeakerName(string name)
    {

        for(int i = 0; i < characters.Count; i++)
        {
            if (characters[i].Name.ToLower().Equals(name.ToLower()))
            {
                return characters[i];
            }
        }

        Console.Error.WriteLine(String.Format("There was a problem looking for the character {0} in the current characters list.\n" +
            "Returning first character in the list."),name);
        return characters[0];
    }

    public void EnableTextBox()
    {
       
    }

    public void DisableTextBox()
    {
        isActive = false;
    }
}
