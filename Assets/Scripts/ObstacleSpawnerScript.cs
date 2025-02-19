using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerScript : MonoBehaviour
{
    public GameObject[] prefab;
    public Vector3 spawnPos;
    public Vector3 spawnPosInv;
    private float timer;
    public float timerCap;
    void Start()
    {
        Instantiate(prefab[0], spawnPos, Quaternion.identity);
    }
    void Update()
    {
        int choice = Random.Range(0, prefab.Length);
        timer += Time.deltaTime;
        if(timer >= timerCap)
        {
            timer = 0;
            if(choice == 1)
                Instantiate(prefab[choice], spawnPosInv, Quaternion.Euler(180, 0, 0));
            else
                Instantiate(prefab[choice], spawnPos, Quaternion.identity);
        }
    }
}
