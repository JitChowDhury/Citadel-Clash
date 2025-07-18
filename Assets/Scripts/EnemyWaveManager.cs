using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public event EventHandler OnWaveNumberChanged;
    private enum State
    {
        WaitingToSpawnNextWave,
        SpawningWave,
    }

    [SerializeField] List<Transform> spawnPositionTranformList;
    [SerializeField] Transform nextWaveSpawnPosition;
    private State state;
    private int waveNumber;
    private float nextWaveSpawnTimer;
    private float nextEnemySpawnTimer;
    private int remainingEnemySpawnAmount;
    private Vector3 spawnPos;
    void Start()
    {
        state = State.WaitingToSpawnNextWave;
        spawnPos = spawnPositionTranformList[UnityEngine.Random.Range(0, spawnPositionTranformList.Count)].position;
        nextWaveSpawnPosition.position = spawnPos;
        nextWaveSpawnTimer = 3f;
    }
    void Update()
    {
        switch (state)
        {
            case State.WaitingToSpawnNextWave:

                nextWaveSpawnTimer -= Time.deltaTime;
                if (nextWaveSpawnTimer < 0f)
                {

                    SpawnWave();
                }
                break;
            case State.SpawningWave:
                if (remainingEnemySpawnAmount > 0)
                {
                    nextEnemySpawnTimer -= Time.deltaTime;
                    if (nextEnemySpawnTimer < 0f)
                    {
                        nextEnemySpawnTimer = UnityEngine.Random.Range(0f, .2f);
                        Enemy.Create(spawnPos + UtilsClass.GetRandomDir() * UnityEngine.Random.Range(0f, 10f));
                        remainingEnemySpawnAmount--;

                        if (remainingEnemySpawnAmount <= 0)
                        {
                            state = State.WaitingToSpawnNextWave;
                            spawnPos = spawnPositionTranformList[UnityEngine.Random.Range(0, spawnPositionTranformList.Count)].position;
                            nextWaveSpawnPosition.position = spawnPos;
                        }


                    }
                }
                break;
        }


    }

    public void SpawnWave()
    {


        nextWaveSpawnTimer = 10f;
        remainingEnemySpawnAmount = 5 + 3 * waveNumber;
        state = State.SpawningWave;
        waveNumber++;
        OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);

    }

    public int GetWaveNumber()
    {
        return waveNumber;
    }

    public float GetNextWaveSpawnTimer()
    {
        return nextWaveSpawnTimer;
    }

}
