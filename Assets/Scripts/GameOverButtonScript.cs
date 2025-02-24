using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverButtonScript : MonoBehaviour
{
    public AudioManagerScript audioScript;
    public GameOverBGMScript gameOverBGMScript;
    public TimerDisplayScript timerDisplayScript;
    public GameObject canvas;
    Text textUII;

    private void Awake()
    {
        audioScript = GameObject.Find("Audio Manager").GetComponent<AudioManagerScript>();
        gameOverBGMScript = GameObject.Find("Canvas").GetComponent<GameOverBGMScript>();
        canvas = GameObject.Find("PersistentCanvas");
        textUII = GameObject.Find("Timer Text").GetComponent<Text>();
    }
    public void PlayAgain()
    {
        audioScript.musicSource.clip = audioScript.mainBgm;
        audioScript.musicSource.volume = gameOverBGMScript.initialVolume;
        Destroy(canvas);
        SceneManager.LoadScene(1);
        audioScript.musicSource.Play();
    }
    public void MainMenu()
    {
        audioScript.musicSource.clip = audioScript.mainBgm;
        audioScript.musicSource.volume = gameOverBGMScript.initialVolume; 
        Destroy(canvas);
        SceneManager.LoadScene(0);
        audioScript.musicSource.Play();
    }
}
