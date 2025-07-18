using System;
using TMPro;
using UnityEngine;

public class EnemyWaveUI : MonoBehaviour
{
    [SerializeField] EnemyWaveManager enemyWaveManager;
    private Camera mainCamera;
    private TextMeshProUGUI waveNumberText;
    private TextMeshProUGUI waveMessageText;

    private RectTransform enemyWaveSpawnPosIndicator;
    private RectTransform enemyClosestPositionIndicator;
    private void Awake()
    {
        waveNumberText = transform.Find("waveNumberText").GetComponent<TextMeshProUGUI>();
        waveMessageText = transform.Find("waveMessageText").GetComponent<TextMeshProUGUI>();
        enemyWaveSpawnPosIndicator = transform.Find("enemyWaveSpawnPosIndicator").GetComponent<RectTransform>();
        enemyClosestPositionIndicator = transform.Find("enemyClosestPositionIndicator").GetComponent<RectTransform>();

    }
    void Start()
    {
        mainCamera = Camera.main;
        enemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveNumberChanged;
        SetWaveNumberText("Wave: " + enemyWaveManager.GetWaveNumber());
    }

    private void EnemyWaveManager_OnWaveNumberChanged(object sender, EventArgs e)
    {
        SetWaveNumberText("Wave: " + enemyWaveManager.GetWaveNumber());
    }

    void Update()
    {
        HandleNextWaveMessage();

        HandleEnemySpawnPositionIndicator();

        HandleEnemyClosestPositionIndicator();

    }

    private void HandleNextWaveMessage()
    {
        float nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();
        if (nextWaveSpawnTimer <= 0f)
        {
            SetMessageText("");
        }
        else
        {
            SetMessageText("Next Wave in " + nextWaveSpawnTimer.ToString("F1") + "s");
        }
    }

    private void HandleEnemySpawnPositionIndicator()
    {
        Vector3 dirToSpawnPosition = (enemyWaveManager.GetSpawnPosition() - mainCamera.transform.position).normalized;
        enemyWaveSpawnPosIndicator.anchoredPosition = dirToSpawnPosition * 300f;
        enemyWaveSpawnPosIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(dirToSpawnPosition));
        float distanceToNextSpawnPos = Vector3.Distance(enemyWaveManager.GetSpawnPosition(), mainCamera.transform.position);
        enemyWaveSpawnPosIndicator.gameObject.SetActive(distanceToNextSpawnPos > mainCamera.orthographicSize * 1.5f);
    }

    private void HandleEnemyClosestPositionIndicator()
    {
        float targetMaxRadius = 999f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(mainCamera.transform.position, targetMaxRadius);

        Enemy targetEnemy = null;


        foreach (Collider2D collider2D in collider2DArray)
        {
            Enemy enemy = collider2D.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (targetEnemy == null)
                {
                    targetEnemy = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, targetEnemy.transform.position))
                    {
                        //closer
                        targetEnemy = enemy;
                    }
                }
            }
        }

        if (targetEnemy != null)
        {
            Vector3 dirToClosestEnemy = (targetEnemy.transform.position - mainCamera.transform.position).normalized;
            enemyClosestPositionIndicator.anchoredPosition = dirToClosestEnemy * 250f;
            enemyClosestPositionIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(dirToClosestEnemy));



            float distanceToClosestEnemy = Vector3.Distance(enemyWaveManager.GetSpawnPosition(), mainCamera.transform.position);
            enemyClosestPositionIndicator.gameObject.SetActive(distanceToClosestEnemy > mainCamera.orthographicSize * 1.5f);
        }
        else
        {
            enemyClosestPositionIndicator.gameObject.SetActive(false);

        }
    }


    private void SetMessageText(string message)
    {
        waveMessageText.SetText(message);
    }

    private void SetWaveNumberText(string text)
    {
        waveNumberText.SetText(text);
    }
}

