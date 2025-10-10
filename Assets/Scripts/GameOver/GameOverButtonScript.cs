using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverButtonScript : MonoBehaviour
{
    public AudioManagerScript AudioScript;
    public GameOverBGMScript GameOverBGMScript;
    public TimerDisplayScript TimerDisplayScript;
    public GameObject Canvas;
    private Text _textUII;

    private void Awake()
    {
        AudioScript = GameObject.Find("Audio Manager").GetComponent<AudioManagerScript>();
        GameOverBGMScript = GameObject.Find("Canvas").GetComponent<GameOverBGMScript>();
        Canvas = GameObject.Find("PersistentCanvas");
        _textUII = GameObject.Find("Timer Text").GetComponent<Text>();
    }
    public void PlayAgain()
    {
        AudioScript.MusicSource.clip = AudioScript.MainBgm;
        AudioScript.MusicSource.volume = GameOverBGMScript.InitialVolume;
        Destroy(Canvas);
        SceneManager.LoadScene(1);
        AudioScript.MusicSource.Play();
    }
    public void MainMenu()
    {
        AudioScript.MusicSource.clip = AudioScript.MainBgm;
        AudioScript.MusicSource.volume = GameOverBGMScript.InitialVolume; 
        Destroy(Canvas);
        SceneManager.LoadScene(0);
        AudioScript.MusicSource.Play();
    }
}
