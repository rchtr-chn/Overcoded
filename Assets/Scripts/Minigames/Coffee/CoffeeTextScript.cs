using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoffeeTextScript : MonoBehaviour
{
    public CoffeeScript coffeeScript;
    public Text prompt;

    private void Start()
    {
        if(coffeeScript == null) GameObject.Find("Coffee").GetComponent<CoffeeScript>();
        if(prompt == null) GetComponent<Text>();
    }

    private void Update()
    {
        if(coffeeScript.CurrentInsanity < 40f && coffeeScript.CurrentInsanity > 0.1f)
            prompt.enabled = true;
        else
            prompt.enabled = false;
    }
}
