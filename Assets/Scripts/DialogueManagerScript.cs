using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;

public class DialogueManagerScript : MonoBehaviour
{
    [Header("Dialogue Panel")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject choicesPanel;
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset[] inkJSONs;
    private Story currentStory;
    private bool isDialogueActive = false;
    private bool isTyping = false;

    private int dialogueIndex = 0;

    void Start() {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach(GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    void Update()
    {
        if (!isDialogueActive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                EnterDialogue(dialogueIndex);
                dialogueIndex = (dialogueIndex + 1) % inkJSONs.Length;
            }
            return;
        }

        if (choicesPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                GameObject selectedChoice = EventSystem.current.currentSelectedGameObject;
                for (int i = 0; i < choices.Length; i++)
                {
                    if (choices[i] == selectedChoice)
                    {
                        MakeChoice(i);
                        break;
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(0) && !isTyping)
        {
            ContinueStory();
        }
    }
    
    public void EnterDialogue(int index)
    {
        currentStory = new Story(inkJSONs[index].text);
        isDialogueActive = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    private void ExitDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            StopAllCoroutines();
            choicesPanel.SetActive(false);
            StartCoroutine(TypeSentence(currentStory.Continue()));
        }
        else if (currentStory.currentChoices.Count > 0)
        {
            DisplayChoices();
        }
        else
        {
            ExitDialogue();
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        isTyping = false;
        
        if (currentStory.currentChoices.Count > 0)
        {
            DisplayChoices();
        }
        else
        {
            yield return new WaitForSeconds(2.0f);
            ExitDialogue();
        }
    }

    private void DisplayChoices()
    {
        choicesPanel.SetActive(true);
        List<Choice> currentChoices = currentStory.currentChoices;

        int index = 0;
        foreach(Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text; 
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0]);
    }

    public void MakeChoice(int choiceIndex)
    {
        if (choiceIndex >= 0 && choiceIndex < currentStory.currentChoices.Count)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            choicesPanel.SetActive(false);
            ContinueStory();
        }
    }
}