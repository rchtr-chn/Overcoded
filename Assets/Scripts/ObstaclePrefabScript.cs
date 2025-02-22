using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstaclePrefabScript : MonoBehaviour
{
    public Rigidbody2D rb;
    private PlayerMovementScript playerMovementScript;
    private ObstacleSpawnerScript obstacleSpawnerScript;
    private Transform spawnerPos;
    void Start()
    {
        playerMovementScript = GameObject.Find("Player").GetComponent<PlayerMovementScript>();
        obstacleSpawnerScript = GameObject.Find("Obstacle-Spawner").GetComponent<ObstacleSpawnerScript>();
        spawnerPos = GameObject.Find("Obstacle-Spawner").GetComponent<Transform>();
    }
    void Update()
    {
        float distance = Vector2.Distance(transform.position, spawnerPos.position);
        if(distance > 20f)
        {
            Destroy(gameObject);
        }

        if(playerMovementScript.isBugged)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            rb.velocity = new Vector2(-obstacleSpawnerScript.prefabSpeed, rb.velocity.y);
        }
    }
}
