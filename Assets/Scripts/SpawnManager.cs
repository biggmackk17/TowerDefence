using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    Wave[] _waves;
    int _waveIndex;
    Wave _currentWave;
    Transform _spawnPoint;
    int _enemyCount; //how many enemies are activated in the scene


    public static Action<int> UIUpdateWave;


    enum SpawnState
    {
        ready,
        spawning,
        waiting
        
    }
    SpawnState _spawnstate;

    private void OnEnable()
    {
        Enemy.EnemyDisable += DecEnemyAlive;
    }

    private void OnDisable()
    {
        Enemy.EnemyDisable -= DecEnemyAlive;
    }

    void Start()
    {
        _spawnstate = SpawnState.ready;
        _spawnPoint = GameObject.Find("SpawnPoint").transform;
        _waveIndex = 0;
        UIUpdateWave?.Invoke(_waveIndex + 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (_spawnstate == SpawnState.ready)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine("StartWave");
            }
        }
    }

    private void DecEnemyAlive()
    {
        _enemyCount--;
        if (_enemyCount <= 0 && _spawnstate == SpawnState.waiting)
        {
            WaveComplete();
        }


    }

    private void WaveComplete()
    {
        _spawnstate = SpawnState.ready;
        Debug.Log("Wave: " + _currentWave.WaveNum + "completed");
        _waveIndex++;
        UIUpdateWave?.Invoke(_waveIndex + 1);
    }

    IEnumerator StartWave()
    {
        _spawnstate = SpawnState.spawning;
        _currentWave = _waves[_waveIndex];
        Debug.Log("Wave: " + _currentWave.WaveNum);
        Debug.Log(_currentWave.name);
        yield return new WaitForSeconds(2f);
        for (var i = 0; i < _currentWave.Enemies.Length; i++)
        {

            //Instantiate(_currentWave.Enemies[i], _spawnPoint.position, Quaternion.identity);
           GameObject __enemy = ObjectPooler.Instance.RequestGameobject(_currentWave.Enemies[i]);
            __enemy.SetActive(false);
            __enemy.transform.position = _spawnPoint.position; // we need to move the object while its inactive so we dont break the navmesh agent
            __enemy.SetActive(true);
            _enemyCount++;
            yield return new WaitForSeconds(_currentWave.Delay);

        }
        _spawnstate = SpawnState.waiting;
        Debug.Log("done spawning");
        yield return null;
    }
}
