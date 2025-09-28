using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySpawnerScript : MonoBehaviour
{
    public GameObject flyPrefab, pointOnePrefab, pointTwoPrefab;
    private GameObject flyActive, pointOneActive, pointTwoActive;
    public WaveScript waveScript;
    public bool isActive = true;
    float timer, timerCap = 10f;

    private Vector2 monitorCodePos = Vector2.zero;
    private Vector2 monitorGamePos = Vector2.zero;

    void Start()
    {
        if(waveScript == null) waveScript = GameObject.Find("Player").GetComponent<WaveScript>();
        monitorCodePos = new Vector2(5, 0);
        monitorGamePos = new Vector2(-4, 0.5f);
    }

    void Update()
    {
        if(waveScript.currentWave < 3) return;

        if(!isActive)
        {
            timer += Time.deltaTime;

            if (timer > timerCap)
            {
                flyActive = null;
                isActive = true;
                timer = 0;
            }
        }

        if (waveScript.currentWave > 3 && flyActive == null)
        {
            flyActive = Instantiate(flyPrefab, transform);
            if(pointOneActive == null)
                pointOneActive = Instantiate(pointOnePrefab, monitorCodePos, Quaternion.identity);
        }
        if(waveScript.currentWave > 7 && pointTwoActive == null)
            pointTwoActive = Instantiate(pointTwoPrefab, monitorGamePos, Quaternion.identity);
    }
}
