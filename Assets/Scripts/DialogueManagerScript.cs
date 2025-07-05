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
    public int triggerTime = 0;
    private int lastWave = -1;

    public WaveScript waveScript;

    void Start() {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];

        waveScript = GameObject.Find("Player").GetComponent<WaveScript>();
        
        int index = 0;
        foreach(GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            EventTrigger trigger = choice.GetComponent<EventTrigger>();
            if (trigger == null)
            {
                trigger = choice.AddComponent<EventTrigger>();
            }
            if(choicesPanel)
            {
                AddEventTrigger(trigger, EventTriggerType.PointerEnter, () => OnMouseHoverChoice(choice));
                AddEventTrigger(trigger, EventTriggerType.PointerClick, () => OnMouseClickChoice(index));
            }
            index++;
        }

        //EnterDialogue(1);
    }

    void Update()
    {
        if (!isDialogueActive)
        {
            if (waveScript.currentWave < 5) return;

            if (waveScript.currentWave != lastWave)
            {
                lastWave = waveScript.currentWave;

                switch (waveScript.currentWave)
                {
                    case 5:
                        triggerTime = UnityEngine.Random.Range(7, 10);
                        StartCoroutine(waitForSeconds(triggerTime, 0));
                        break;
                    case 8:
                        triggerTime = UnityEngine.Random.Range(10, 15);
                        StartCoroutine(waitForSeconds(triggerTime, 1));
                        break;
                    case 9:
                        triggerTime = UnityEngine.Random.Range(15, 20);
                        StartCoroutine(waitForSeconds(triggerTime, 2));
                        break;
                    case 10:
                        triggerTime = UnityEngine.Random.Range(20, 25);
                        StartCoroutine(waitForSeconds(triggerTime, 3));
                        break;
                    case 11:
                        triggerTime = UnityEngine.Random.Range(22, 27);
                        StartCoroutine(waitForSeconds(triggerTime, 4));
                        break;
                    default:
                        triggerTime = UnityEngine.Random.Range(25, 30);
                        dialogueIndex = UnityEngine.Random.Range(0, 5);
                        StartCoroutine(waitForSeconds(triggerTime, dialogueIndex));
                        break;
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(!isTyping && currentStory != null)
                ContinueStory();
        }
    }

    private IEnumerator waitForSeconds(int triggerTime, int dialogueIndex)
    {
        yield return new WaitForSeconds(triggerTime);
        EnterDialogue(dialogueIndex);
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
        if (currentStory == null)
        {
            return;
        }

        if(currentStory.canContinue)
        {
            StopAllCoroutines();
            choicesPanel.SetActive(false);
            StartCoroutine(TypeSentence(currentStory.Continue()));
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
        DisplayChoices();
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
        currentStory.ChooseChoiceIndex(choiceIndex);
    }

    private void AddEventTrigger(EventTrigger trigger, EventTriggerType eventType, Action action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener((eventData) => action());
        trigger.triggers.Add(entry);
    }

    private void OnMouseHoverChoice(GameObject choice)
    {
        EventSystem.current.SetSelectedGameObject(choice);
    }

    private void OnMouseClickChoice(int index)
    {
        MakeChoice(index);
    }
}