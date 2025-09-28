using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerScript : MonoBehaviour
{
    public GameObject[] prefab;
    public Vector3 spawnPos;
    public Vector3 spawnPosInv;
    public float prefabSpeed = 5f;
    private float timer;
    public float timerCap;
    public int choice;

    public PlayerMovementScript isBugScript;
    public WaveScript waveScript;
    void Start()
    {
        //Instantiate(prefab[0], spawnPos, Quaternion.identity);
        if(isBugScript == null)
            isBugScript = GameObject.Find("Player").GetComponent<PlayerMovementScript>();
        if(waveScript == null)
            waveScript = GameObject.Find("Player").GetComponent<WaveScript>();
    }
    void Update()
    {
        if (isBugScript.isBugged) return;

        if (waveScript.currentWave > 1)
            choice = Random.Range(0, prefab.Length);
        else
            choice = 0;

        timer += Time.deltaTime;
        if (timer >= timerCap)
        {
            timer = 0;
            //if (choice == 1)
                //Instantiate(prefab[choice], spawnPosInv, Quaternion.Euler(180, 0, 0));
            //else
                //Instantiate(prefab[choice], spawnPos, Quaternion.identity);
        }
    }
}
