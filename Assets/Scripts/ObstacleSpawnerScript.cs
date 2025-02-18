using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerScript : MonoBehaviour
{
    public GameObject prefab;
    private float timer;
    public float timerCap;
    void Start()
    {
        Instantiate(prefab, this.transform);
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= timerCap)
        {
            timer = 0;
            Instantiate(prefab, this.transform);
        }
    }
}
