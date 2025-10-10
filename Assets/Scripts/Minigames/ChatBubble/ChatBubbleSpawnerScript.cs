using System.Collections.Generic;
using UnityEngine;

public class ChatBubbleSpawnerScript : MonoBehaviour
{
    public GameObject Prefab;
    public List<GameObject> ToSpawn = new List<GameObject>();
    public WaveScript WaveScript;
    private int _mockWave = 0;
    public int AvailablePos = 4;
    public SpriteRenderer ScreenSprite;

    public void Start()
    {
        WaveScript = GameObject.Find("Player").GetComponent<WaveScript>();
        ScreenSprite = GameObject.Find("Screen").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (WaveScript.CurrentWave < 7) return;

        ScreenSprite.enabled = true;
        if (_mockWave < WaveScript.CurrentWave && WaveScript.CurrentWave > 6)
        {
            if (WaveScript.CurrentWave == 7)
            {
                _mockWave = WaveScript.CurrentWave;
                for (int i = 0; i < 2; i++)
                {
                    ToSpawn.Add(Prefab);
                }
            }
            else if (WaveScript.CurrentWave == 8)
            {
                _mockWave = WaveScript.CurrentWave;
                for (int i = 0; i < 3; i++)
                {
                    ToSpawn.Add(Prefab);
                }
            }
            else if (WaveScript.CurrentWave == 9)
            {
                _mockWave = WaveScript.CurrentWave;
                for (int i = 0; i < 5; i++)
                {
                    ToSpawn.Add(Prefab);
                }
            }
            else if (WaveScript.CurrentWave >= 10)
            {
                _mockWave = WaveScript.CurrentWave;
                for (int i = 0; i < 7 + 2 * (WaveScript.CurrentWave - 10); i++)
                {
                    ToSpawn.Add(Prefab);
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
        if (AvailablePos == 0) return;

        for(int i = 0 ; i < ToSpawn.Count ; i++)
        {
            if (ToSpawn.Contains(Prefab) && AvailablePos > 0)
            {
                AvailablePos--;
                GameObject chatBubble = (GameObject)Instantiate(ToSpawn[i],transform.position,Quaternion.identity);
                ToSpawn.RemoveAt(0);
            }
        }
    }

}
