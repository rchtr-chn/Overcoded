using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    // WORDBANK----


    public Text wordOutput;
    public Text prompt;
    private string remainingWord = string.Empty;
    private string currentString = "test";
    private string inputString = string.Empty;

    private void Start()
    {
        //TEST, REMOVE AFTER ADDING WORDBANK
        //--------

        SetCurrentWord();
    }

    private void SetCurrentWord()
    {
        //GET STRING FROM WORDBANK
        SetRemainingWord(currentString);
    }

    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        prompt.text = currentString;
        wordOutput.text = inputString;
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if(Input.anyKeyDown)
        {

            string keyPressed = Input.inputString;

            if(keyPressed.Length == 1 )
            {
                EnterLetter(keyPressed);
            }
        }
    }

    private void EnterLetter(string typedLetter)
    {
        if(IsCorrectLetter(typedLetter))
        {
            AddLetter(typedLetter);
            RemoveLetter();

            if(IsComplete())
            {
                inputString = string.Empty;
                SetCurrentWord();
            }
        }
    }

    private bool IsCorrectLetter(string letter)
    {
        return remainingWord.IndexOf(letter) == 0;
    }

    private void AddLetter(string typedLetter)
    {
        inputString = inputString + typedLetter;
    }

    private void RemoveLetter()
    {
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }

    private bool IsComplete()
    {
        return remainingWord.Length == 0;
    }
}
