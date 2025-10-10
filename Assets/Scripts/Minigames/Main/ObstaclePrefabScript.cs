using UnityEngine;

public class ObstaclePrefabScript : MonoBehaviour
{
    public Rigidbody2D Rb;
    private PlayerMovementScript _playerMovementScript;
    private ObstacleSpawnerScript _obstacleSpawnerScript;
    private Transform _spawnerPos;
    void Start()
    {
        _playerMovementScript = GameObject.Find("Player").GetComponent<PlayerMovementScript>();
        _obstacleSpawnerScript = GameObject.Find("Obstacle-Spawner").GetComponent<ObstacleSpawnerScript>();
        _spawnerPos = GameObject.Find("Obstacle-Spawner").GetComponent<Transform>();
    }
    void Update()
    {
        float distance = Vector2.Distance(transform.position, _spawnerPos.position);
        if(distance > 20f)
        {
            Destroy(gameObject);
        }

        //if(playerMovementScript.isBugged)
        //{
        //    rb.velocity = Vector3.zero;
        //}
        //else
        //{
            Rb.velocity = new Vector2(-_obstacleSpawnerScript.PrefabSpeed, Rb.velocity.y);
        //}
    }
}
