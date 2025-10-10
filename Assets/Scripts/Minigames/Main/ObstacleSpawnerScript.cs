using UnityEngine;

public class ObstacleSpawnerScript : MonoBehaviour
{
    public GameObject[] Prefab;
    public Vector3 SpawnPos = new Vector3(1f, -0.7f, 0f);
    public Vector3 SpawnPosInv = new Vector3(1f, 0.2f, 0f);
    public float PrefabSpeed = 5f;
    private float _timer;
    public float TimerCap = 2f;
    public int Choice;

    public PlayerMovementScript IsBugScript;
    public WaveScript WaveScript;
    void Start()
    {
        //Instantiate(prefab[0], spawnPos, Quaternion.identity);
        if(IsBugScript == null)
            IsBugScript = GameObject.Find("Player").GetComponent<PlayerMovementScript>();
        if(WaveScript == null)
            WaveScript = GameObject.Find("Player").GetComponent<WaveScript>();
    }
    void Update()
    {
        if (IsBugScript.IsBugged) return;

        if (WaveScript.CurrentWave > 1)
            Choice = Random.Range(0, Prefab.Length);
        else
            Choice = 0;

        _timer += Time.deltaTime;
        if (_timer >= TimerCap)
        {
            _timer = 0;
            //if (choice == 1)
                //Instantiate(prefab[choice], spawnPosInv, Quaternion.Euler(180, 0, 0));
            //else
                //Instantiate(prefab[choice], spawnPos, Quaternion.identity);
        }
    }
}
