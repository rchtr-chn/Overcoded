using UnityEngine;
using UnityEngine.UI;

public class HighScoreScript : MonoBehaviour
{
    public Text HighScoreText;
    private int _highScore;

    void Start()
    {
        HighScoreText = GetComponent<Text>();
        LoadHighScore();
    }

    public void SaveHighScore(int score)
    {
        if (score > _highScore)
        {
            _highScore = score;
            PlayerPrefs.SetInt("HighScore", _highScore);
            PlayerPrefs.Save();
        }
    }

    public void LoadHighScore()
    {
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
        int seconds = _highScore % 60;
        int minutes = _highScore / 60;
        int hours = _highScore / 3600;
        HighScoreText.text = string.Format("High Score: {0:00}:{1:00}:{2:00}", hours, minutes, seconds); ;
    }
}
