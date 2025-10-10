using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySpawnerScript : MonoBehaviour
{
    public GameObject FlyPrefab, PointOnePrefab, PointTwoPrefab;
    private GameObject _flyActive, _pointOneActive, _pointTwoActive;
    public WaveScript WaveScript;
    public bool IsActive = true;
    private float _timer, _timerCap = 10f;

    private Vector2 _monitorCodePos = Vector2.zero;
    private Vector2 _monitorGamePos = Vector2.zero;

    void Start()
    {
        if(WaveScript == null) WaveScript = GameObject.Find("Player").GetComponent<WaveScript>();
        _monitorCodePos = new Vector2(5, 0);
        _monitorGamePos = new Vector2(-4, 0.5f);
    }

    void Update()
    {
        if(WaveScript.CurrentWave < 3) return;

        if(!IsActive)
        {
            _timer += Time.deltaTime;

            if (_timer > _timerCap)
            {
                _flyActive = null;
                IsActive = true;
                _timer = 0;
            }
        }

        if (WaveScript.CurrentWave > 3 && _flyActive == null)
        {
            _flyActive = Instantiate(FlyPrefab, transform);
            if(_pointOneActive == null)
                _pointOneActive = Instantiate(PointOnePrefab, _monitorCodePos, Quaternion.identity);
        }
        if(WaveScript.CurrentWave > 7 && _pointTwoActive == null)
            _pointTwoActive = Instantiate(PointTwoPrefab, _monitorGamePos, Quaternion.identity);
    }
}
