using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChatBubbleSpawnerScript : MonoBehaviour
{
    public GameObject prefab;
    public List<GameObject> toSpawn = new List<GameObject>();
    public WaveScript waveScript;
    int mockWave = 0;
    public int availablePos = 4;

    public void Start()
    {
        waveScript = GameObject.Find("Player").GetComponent<WaveScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waveScript.currentWave < 7) return;

        if (mockWave < waveScript.currentWave && waveScript.currentWave > 6)
        {
            if (waveScript.currentWave == 7)
            {
                mockWave = waveScript.currentWave;
                for (int i = 0; i < 2; i++)
                {
                    toSpawn.Add(prefab);
                }
            }
            else if (waveScript.currentWave == 8)
            {
                mockWave = waveScript.currentWave;
                for (int i = 0; i < 3; i++)
                {
                    toSpawn.Add(prefab);
                }
            }
            else if (waveScript.currentWave == 9)
            {
                mockWave = waveScript.currentWave;
                for (int i = 0; i < 5; i++)
                {
                    toSpawn.Add(prefab);
                }
            }
            else if (waveScript.currentWave >= 10)
            {
                mockWave = waveScript.currentWave;
                for (int i = 0; i < 7 + 2 * (waveScript.currentWave - 10); i++)
                {
                    toSpawn.Add(prefab);
                }
            }
        }

        SpawnEnt();
    }

    void SpawnEnt()
    {
        for(int i = 0 ; i < toSpawn.Count ; i++)
        {
            if (availablePos > 0 && toSpawn.Contains(prefab))
            {
                GameObject chatBubble = (GameObject)Instantiate(toSpawn[i]);
                toSpawn.RemoveAt(0);
            }
        }
    }

}
