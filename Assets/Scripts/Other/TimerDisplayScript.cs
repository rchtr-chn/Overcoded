using UnityEngine;
using UnityEngine.UI;

public class TimerDisplayScript : MonoBehaviour
{
    public static TimerDisplayScript Instance = null;
    public WaveScript WaveScript;
    public PlayerMovementScript PlayerMovementScript;
    public HighScoreScript HighScoreScript;
    public float Timer = 0, Counter;
    private float _seconds, _minutes, _hours;
    public Text TextUI;
    // Start is called before the first frame update
    void Start()
    {
        if (WaveScript == null)
            WaveScript = GameObject.Find("Player").GetComponent<WaveScript>();
        if (PlayerMovementScript == null)
            PlayerMovementScript = GameObject.Find("Player").GetComponent<PlayerMovementScript>();

        TextUI = GameObject.Find("Timer Text").GetComponent<Text>();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovementScript.IsAlive && PlayerMovementScript != null)
        {
            Timer = WaveScript.TotalElapsedTime;
            _seconds = Mathf.FloorToInt(Timer % 60);
            if (Timer > 60)
            {
                _minutes = Mathf.FloorToInt(Timer / 60);
            }
            if (Timer > 3600)
                _hours = Mathf.FloorToInt(Timer / (60 * 60));

            TextUI.text = string.Format("{0:00}:{1:00}:{2:00}", _hours, _minutes, _seconds);
        }
        else
        {
            if (HighScoreScript == null)
                HighScoreScript = GameObject.Find("PlayerPrefs").GetComponent<HighScoreScript>();

            HighScoreScript.SaveHighScore(Mathf.FloorToInt(WaveScript.TotalElapsedTime));
            HighScoreScript.LoadHighScore();

            Counter += Time.deltaTime;

            if (Counter > 1)
                TextUI.color = Color.red;
        }
    }
}
