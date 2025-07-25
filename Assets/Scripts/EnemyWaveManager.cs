using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public static EnemyWaveManager Instance { get; private set; }
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

    void Awake()
    {
        Instance = this;
    }
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

                    SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyWaveStarting);
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
                            nextWaveSpawnTimer = 10f;
                        }


                    }
                }
                break;
        }


    }

    public void SpawnWave()
    {



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

    public Vector3 GetSpawnPosition()
    {
        return spawnPos;
    }

}
