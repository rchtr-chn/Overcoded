using UnityEngine;
using UnityEngine.UI;

public class HighScoreScript : MonoBehaviour
{
    public Text highScoreText;
    private int highScore;

    void Start()
    {
        highScoreText = GetComponent<Text>();
        LoadHighScore();
    }

    public void SaveHighScore(int score)
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }

    public void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        int seconds = highScore % 60;
        int minutes = highScore / 60;
        int hours = highScore / 3600;
        highScoreText.text = string.Format("High Score: {0:00}:{1:00}:{2:00}", hours, minutes, seconds); ;
    }
}
