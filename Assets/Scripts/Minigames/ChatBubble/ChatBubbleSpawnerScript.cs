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
    public SpriteRenderer screenSprite;

    public void Start()
    {
        waveScript = GameObject.Find("Player").GetComponent<WaveScript>();
        screenSprite = GameObject.Find("Screen").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waveScript.currentWave < 7) return;

        screenSprite.enabled = true;
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

    //void TestFunctionality()
    //{
    //    screenSprite.enabled = true;
    //    for (int i=0;i<5;i++)
    //    {
    //        toSpawn.Add(prefab);
    //    }
    //}

    void SpawnEnt()
    {
        if (availablePos == 0) return;

        for(int i = 0 ; i < toSpawn.Count ; i++)
        {
            if (toSpawn.Contains(prefab) && availablePos > 0)
            {
                availablePos--;
                GameObject chatBubble = (GameObject)Instantiate(toSpawn[i],transform.position,Quaternion.identity);
                toSpawn.RemoveAt(0);
            }
        }
    }

}
