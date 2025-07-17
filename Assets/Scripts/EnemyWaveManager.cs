using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    private float nextWaveSpawnTimer;
    private float nextEnemySpawnTimer;
    private int remainingEnemySpawnAmount;
    private Vector3 spawnPos;
    void Start()
    {
        nextWaveSpawnTimer = 3f;
    }

    void Update()
    {
        nextWaveSpawnTimer -= Time.deltaTime;
        if (nextWaveSpawnTimer < 0f)
        {

            SpawnWave();
        }

        if (remainingEnemySpawnAmount > 0)
        {
            nextEnemySpawnTimer -= Time.deltaTime;
            if (nextEnemySpawnTimer < 0f)
            {
                nextEnemySpawnTimer = Random.Range(0f, .2f);
                Enemy.Create(spawnPos + UtilsClass.GetRandomDir() * Random.Range(0f, 10f));
                remainingEnemySpawnAmount--;

            }
        }
    }

    public void SpawnWave()
    {
        Vector3 spawnPos = new Vector3(40, 0);

        nextWaveSpawnTimer = 10f;
        remainingEnemySpawnAmount = 10;

    }

}
