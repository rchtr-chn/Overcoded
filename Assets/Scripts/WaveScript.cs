using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour
{
    public AudioManagerScript audioScript;
    public float totalElapsedTime = 0f;
    public int currentWave;
    public bool isWave = false;

    private void Start()
    {
        if (audioScript == null) audioScript = GameObject.Find("Audio Manager").GetComponent<AudioManagerScript>();
    }
    void Update()
    {
        if(currentWave == 10 && !isWave)
        {
            audioScript.musicSource.Stop();
            audioScript.musicSource.clip = audioScript.mainBgmChaotic;
            audioScript.musicSource.Play();
            isWave = true;
        }
        totalElapsedTime += Time.deltaTime;
        currentWave = GetCurrentWave(totalElapsedTime);
        //currentWave = 10;
        Debug.Log($"Current Wave: {currentWave} | Elapsed Time: {totalElapsedTime:F2} sec");
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
