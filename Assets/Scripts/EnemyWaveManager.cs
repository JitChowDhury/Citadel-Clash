using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{

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
        spawnPos = spawnPositionTranformList[Random.Range(0, spawnPositionTranformList.Count)].position;
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
                        nextEnemySpawnTimer = Random.Range(0f, .2f);
                        Enemy.Create(spawnPos + UtilsClass.GetRandomDir() * Random.Range(0f, 10f));
                        remainingEnemySpawnAmount--;

                        if (remainingEnemySpawnAmount <= 0)
                        {
                            state = State.WaitingToSpawnNextWave;
                            spawnPos = spawnPositionTranformList[Random.Range(0, spawnPositionTranformList.Count)].position;
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

    }

}
