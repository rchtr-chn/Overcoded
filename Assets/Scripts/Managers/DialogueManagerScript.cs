using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using System;

public class DialogueManagerScript : MonoBehaviour
{
    [Header("Dialogue Panel")]
    [SerializeField] private GameObject DialoguePanel;
    [SerializeField] private TextMeshProUGUI DialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject ChoicesPanel;
    [SerializeField] private GameObject[] Choices;
    private TextMeshProUGUI[] _choicesText;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset[] InkJSONs;
    private Story _currentStory;
    private bool _isDialogueActive = false;
    private bool _isTyping = false;

    private int _dialogueIndex = 0;
    public int TriggerTime = 0;
    private int _lastWave = -1;

    public WaveScript WaveScript;

    void Start() {
        _isDialogueActive = false;
        DialoguePanel.SetActive(false);

        _choicesText = new TextMeshProUGUI[Choices.Length];

        WaveScript = GameObject.Find("Player").GetComponent<WaveScript>();
        
        int index = 0;
        foreach(GameObject choice in Choices)
        {
            _choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            EventTrigger trigger = choice.GetComponent<EventTrigger>();
            if (trigger == null)
            {
                trigger = choice.AddComponent<EventTrigger>();
            }
            if(ChoicesPanel)
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
        if (!_isDialogueActive)
        {
            if (WaveScript.CurrentWave < 5) return;

            if (WaveScript.CurrentWave != _lastWave)
            {
                _lastWave = WaveScript.CurrentWave;

                switch (WaveScript.CurrentWave)
                {
                    case 5:
                        TriggerTime = UnityEngine.Random.Range(7, 10);
                        StartCoroutine(waitForSeconds(TriggerTime, 0));
                        break;
                    case 8:
                        TriggerTime = UnityEngine.Random.Range(10, 15);
                        StartCoroutine(waitForSeconds(TriggerTime, 1));
                        break;
                    case 9:
                        TriggerTime = UnityEngine.Random.Range(15, 20);
                        StartCoroutine(waitForSeconds(TriggerTime, 2));
                        break;
                    case 10:
                        TriggerTime = UnityEngine.Random.Range(20, 25);
                        StartCoroutine(waitForSeconds(TriggerTime, 3));
                        break;
                    case 11:
                        TriggerTime = UnityEngine.Random.Range(22, 27);
                        StartCoroutine(waitForSeconds(TriggerTime, 4));
                        break;
                    default:
                        TriggerTime = UnityEngine.Random.Range(25, 30);
                        _dialogueIndex = UnityEngine.Random.Range(0, 5);
                        StartCoroutine(waitForSeconds(TriggerTime, _dialogueIndex));
                        break;
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(!_isTyping && _currentStory != null)
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
        _currentStory = new Story(InkJSONs[index].text);
        _isDialogueActive = true;
        DialoguePanel.SetActive(true);

        ContinueStory();
    }

    private void ExitDialogue()
    {
        _isDialogueActive = false;
        DialoguePanel.SetActive(false);
        DialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (_currentStory == null)
        {
            return;
        }

        if(_currentStory.canContinue)
        {
            StopAllCoroutines();
            ChoicesPanel.SetActive(false);
            StartCoroutine(TypeSentence(_currentStory.Continue()));
        }
        else
        {
            ExitDialogue();
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        _isTyping = true;
        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        _isTyping = false;
        DisplayChoices();
    }

    private void DisplayChoices()
    {
        ChoicesPanel.SetActive(true);
        List<Choice> currentChoices = _currentStory.currentChoices;

        int index = 0;
        foreach(Choice choice in currentChoices)
        {
            Choices[index].gameObject.SetActive(true);
            _choicesText[index].text = choice.text; 
            index++;
        }

        for (int i = index; i < Choices.Length; i++)
        {
            Choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(Choices[0]);
    }

    public void MakeChoice(int choiceIndex)
    {
        _currentStory.ChooseChoiceIndex(choiceIndex);
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