using UnityEngine;

public class WaveScript : MonoBehaviour
{
    public AudioManagerScript AudioScript;
    public float TotalElapsedTime = 0f;
    public int CurrentWave;
    public bool IsWave = false;

    private void Start()
    {
        if (AudioScript == null) AudioScript = GameObject.Find("Audio Manager").GetComponent<AudioManagerScript>();
    }
    void Update()
    {
        if(CurrentWave == 7 && !IsWave)
        {
            AudioScript.MusicSource.Stop();
            AudioScript.MusicSource.clip = AudioScript.MainBgmChaotic;
            AudioScript.MusicSource.Play();
            IsWave = true;
        }
        TotalElapsedTime += Time.deltaTime;
        CurrentWave = GetCurrentWave(TotalElapsedTime);
        // currentWave = 5;
        //Debug.Log($"Current Wave: {currentWave} | Elapsed Time: {totalElapsedTime:F2} sec");
    }

    int GetCurrentWave(float elapsedTime)
    {
        float timeTracker = 0f;

        // Wave 1 (10 sec)
        if (elapsedTime < 10f)
            return 1;
        timeTracker += 10f;

        // Waves 2-4 (15 sec each)
        for (int wave = 2; wave <= 4; wave++)
        {
            if (elapsedTime < timeTracker + 15f)
                return wave;
            timeTracker += 15f;
        }

        // Waves 5-7 (20 sec each)
        for (int wave = 5; wave <= 7; wave++)
        {
            if (elapsedTime < timeTracker + 20f)
                return wave;
            timeTracker += 20f;
        }

        // Wave 8 (25 sec)
        if (elapsedTime < timeTracker + 25f)
            return 8;
        timeTracker += 25f;

        // Wave 9+ (incrementing by 5 sec per wave)
        int waveNum = 9;
        float nextWaveDuration = 30f;

        while (elapsedTime >= timeTracker + nextWaveDuration)
        {
            timeTracker += nextWaveDuration;
            nextWaveDuration += 5f;
            waveNum++;
        }

        return waveNum;
    }
}
