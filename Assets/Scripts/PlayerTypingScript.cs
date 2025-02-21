using UnityEngine;
using UnityEngine.UI;

public class PlayerTypingScript : MonoBehaviour
{
    //WORDBANK NEEDED~!!!!

    public Text promptText;
    public Text inputText;
    public PlayerMovementScript playerMovementScript;

    private string currentPrompt = string.Empty;
    private string currentInput = string.Empty;
    void Start()
    {

        //initialize coding minigame, CHANGE WHEN WORDBANK IS ADDED
        GetNewPrompt("player.health -= damage / defense");
    }

    void Update()
    {
        TypeInput();
    }

    private void GetNewPrompt(string word)
    {
        currentPrompt = word;
        promptText.text = currentPrompt;
    }

    private void TypeInput()
    {
        foreach (char c in Input.inputString)
        {

            if (c == '\n' || c == '\r')
            {
                if (CodeValidity())
                {
                    playerMovementScript.isBugged = false;

                    //CHECKS ERROR IN CODE INPUT
                }

                currentInput = string.Empty;
                inputText.text = currentInput;

                //GET NEW PROMPT HERE!!!!
                return;
            }
            else if (c == '\b' && currentInput.Length > 0)
            {
                currentInput = currentInput.Substring(0, currentInput.Length - 1);
                inputText.text = currentInput;
                return;
            }

            if (currentInput.Length < currentPrompt.Length)
            {
                currentInput = currentInput + c;
                inputText.text = currentInput;
            }

        }
    }

    private bool CodeValidity()
    {
        if(currentInput == currentPrompt)
        {
            return false;
        }
        return true;
    }
}