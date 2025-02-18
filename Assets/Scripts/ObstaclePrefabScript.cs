using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePrefabScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    private PlayerMovementScript playerMovementScript;
    void Start()
    {
        playerMovementScript = GameObject.Find("Player").GetComponent<PlayerMovementScript>();
    }
    void Update()
    {
        if(playerMovementScript.isBugged)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }
}
