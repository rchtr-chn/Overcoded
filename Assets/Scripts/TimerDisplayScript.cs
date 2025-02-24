using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class TimerDisplayScript : MonoBehaviour
{
    public static TimerDisplayScript instance = null;
    public WaveScript waveScript;
    public PlayerMovementScript playerMovementScript;
    private float timer = 0, counter;
    float seconds, minutes, hours;
    public Text textUI;
    // Start is called before the first frame update
    void Start()
    {
        if (waveScript == null)
            waveScript = GameObject.Find("Player").GetComponent<WaveScript>();
        if(playerMovementScript == null)
            playerMovementScript = GameObject.Find("Player").GetComponent<PlayerMovementScript>();
        textUI = GameObject.Find("Timer Text").GetComponent<Text>();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovementScript.isAlive && playerMovementScript != null)
        {
            timer = waveScript.totalElapsedTime;
            seconds = Mathf.FloorToInt(timer % 60);
            if (timer > 60)
            {
                minutes = Mathf.FloorToInt(timer / 60);
            }
            if (timer > 3600)
                hours = Mathf.FloorToInt(timer / (60 * 60));

            textUI.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }
        else
        {
            counter += Time.deltaTime;

            if (counter > 1)
                textUI.color = Color.red;
        }
    }
}
