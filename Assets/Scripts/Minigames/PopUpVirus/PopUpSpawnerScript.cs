using System.Collections.Generic;
using UnityEngine;

public class PopUpSpawnerScript : MonoBehaviour
{
    public float SpawnRadius = 100f;
    private Vector2 _spawnPosition;
    private Vector2 _spawnerPos;
    public Transform CanvasTransform;
    public List<GameObject> SpawnList = new List<GameObject>();
    public GameObject Prefab;
    public WaveScript WaveScript;
    private int _mockwave = 0;
    private float _timer = 0;
    private float _timerCap;
    void Start()
    {
        if (CanvasTransform == null) GetComponent<Transform>();
        if(WaveScript == null) WaveScript = GameObject.Find("Player").GetComponent<WaveScript>();
        _spawnerPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (WaveScript.CurrentWave < 4) return;

        switch(WaveScript.CurrentWave)
        {
            case 4:
                _timerCap = 7f;
                break;
            case 9:
                _timerCap = 8f;
                break;
            case 10:
                _timerCap = 9f;
                break;
        }


        _timer += Time.deltaTime;
        if (_timer > _timerCap)
        {
            _mockwave = 0;
            _timer = 0f;
        }

        if (_mockwave < WaveScript.CurrentWave && WaveScript.CurrentWave > 3)
        {
            if(WaveScript.CurrentWave > 3 && WaveScript.CurrentWave < 8)
            {
                _mockwave = WaveScript.CurrentWave;
                for(int i = 0; i < 1;i++)
                {
                    SpawnList.Add(Prefab);
                }
            }
            else if(WaveScript.CurrentWave > 7 && WaveScript.CurrentWave < 9)
            {
                _mockwave = WaveScript.CurrentWave;
                for (int i = 0; i < 2; i++)
                {
                    SpawnList.Add(Prefab);
                }
            }
            else if (WaveScript.CurrentWave == 9)
            {
                _mockwave = WaveScript.CurrentWave;
                for (int i = 0; i < 2; i++)
                {
                    SpawnList.Add(Prefab);
                }
            }
            else if (WaveScript.CurrentWave == 10)
            {
                _mockwave = WaveScript.CurrentWave;
                for (int i = 0; i < 3; i++)
                {
                    SpawnList.Add(Prefab);
                }
            }
            else if (WaveScript.CurrentWave > 10)
            {
                int j = 0;
                _mockwave = WaveScript.CurrentWave;
                for (int i = 0; i < 3 + 1 * WaveScript.CurrentWave - 10; i++)
                {
                    SpawnList.Add(Prefab);
                    j++;
                    if (j == 10) return;
                }
            }
        }

        SpawnEnt();
    }

    void SpawnEnt()
    {
        for (int i = 0; i < SpawnList.Count; i++)
        {
            if (SpawnList.Contains(Prefab))
            {
                GameObject popUp = (GameObject)Instantiate(SpawnList[i], CanvasTransform);
                RectTransform transform = popUp.GetComponent<RectTransform>();
                _spawnPosition = Random.insideUnitCircle * SpawnRadius;
                transform.anchoredPosition = _spawnPosition;
                popUp.transform.localScale = Vector3.one;

                SpawnList.RemoveAt(0);
            }
        }
    }
}
