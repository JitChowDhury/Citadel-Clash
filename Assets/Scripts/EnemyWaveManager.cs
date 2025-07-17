using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    private float nextWaveSpawnTimer;
    void Start()
    {
        SpawnWave();
    }

    void Update()
    {
        nextWaveSpawnTimer -= Time.deltaTime;
        if (nextWaveSpawnTimer < 0f)
        {
            SpawnWave();
        }
    }

    public void SpawnWave()
    {
        Vector3 spawnPos = new Vector3(20, 0);
        for (int i = 0; i < 10; i++)
        {
            Enemy.Create(spawnPos + UtilsClass.GetRandomDir() * Random.Range(0f, 10f));
        }

        nextWaveSpawnTimer = 10f;
    }

}
