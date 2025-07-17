using UnityEngine;

public class Tower : MonoBehaviour
{

    private Enemy targetEnemy;
    private float shootTimer;
    [SerializeField] private float shootTimerMax;
    private float lookForTargetTimer;
    private float lookForTargetTimerMax = .2f;
    private Vector3 projectileSpawnPos;

    void Awake()
    {
        projectileSpawnPos = transform.Find("ProjectileSpawnPos").position;
    }

    void Update()
    {
        HandleTargeting();
        HandleShooting();
    }
    private void HandleTargeting()
    {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer < 0f)
        {
            lookForTargetTimer += lookForTargetTimerMax;
            LookForTargets();
        }
    }

    private void HandleShooting()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            shootTimer += shootTimerMax;
            if (targetEnemy != null)
            {
                ArrowProjectile.Create(projectileSpawnPos, targetEnemy);
            }
        }

    }
    private void LookForTargets()
    {
        float targetMaxRadius = 10f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);


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

    }
}
