using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpSpawnerScript : MonoBehaviour
{
    public float spawnRadius;
    private Vector2 spawnPosition;
    private Vector2 spawnerPos;
    public Transform canvasTransform;
    public List<GameObject> spawnList = new List<GameObject>();
    public GameObject prefab;
    public WaveScript waveScript;
    int mockwave = 0;
    float timer = 0;
    float timerCap;
    void Start()
    {
        if (canvasTransform == null) GetComponent<Transform>();
        if(waveScript == null) waveScript = GameObject.Find("Player").GetComponent<WaveScript>();
        spawnerPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (waveScript.currentWave < 4) return;

        switch(waveScript.currentWave)
        {
            case 4:
                timerCap = 7f;
                break;
            case 9:
                timerCap = 8f;
                break;
            case 10:
                timerCap = 9f;
                break;
        }


        timer += Time.deltaTime;
        if (timer > timerCap)
        {
            mockwave = 0;
            timer = 0f;
        }

        if (mockwave < waveScript.currentWave && waveScript.currentWave > 3)
        {
            if(waveScript.currentWave > 3 && waveScript.currentWave < 8)
            {
                mockwave = waveScript.currentWave;
                for(int i = 0; i < 2;i++)
                {
                    spawnList.Add(prefab);
                }
            }
            else if(waveScript.currentWave > 7 && waveScript.currentWave < 9)
            {
                mockwave = waveScript.currentWave;
                for (int i = 0; i < 3; i++)
                {
                    spawnList.Add(prefab);
                }
            }
            else if (waveScript.currentWave == 9)
            {
                mockwave = waveScript.currentWave;
                for (int i = 0; i < 4; i++)
                {
                    spawnList.Add(prefab);
                }
            }
            else if (waveScript.currentWave == 10)
            {
                mockwave = waveScript.currentWave;
                for (int i = 0; i < 5; i++)
                {
                    spawnList.Add(prefab);
                }
            }
            else if (waveScript.currentWave > 10)
            {
                int j = 0;
                mockwave = waveScript.currentWave;
                for (int i = 0; i < 5 + 1 * waveScript.currentWave - 10; i++)
                {
                    spawnList.Add(prefab);
                    j++;
                    if (j == 10) return;
                }
            }
        }

        SpawnEnt();
    }

    void SpawnEnt()
    {
        for (int i = 0; i < spawnList.Count; i++)
        {
            if (spawnList.Contains(prefab))
            {
                GameObject popUp = (GameObject)Instantiate(spawnList[i], canvasTransform);
                RectTransform transform = popUp.GetComponent<RectTransform>();
                spawnPosition = Random.insideUnitCircle * spawnRadius;
                transform.anchoredPosition = spawnPosition;
                popUp.transform.localScale = Vector3.one;

                spawnList.RemoveAt(0);
            }
        }
    }
}
