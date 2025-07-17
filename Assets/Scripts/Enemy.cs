using Mono.Cecil;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Enemy : MonoBehaviour
{
    public static Enemy Create(Vector3 pos)
    {
        Transform pfEnemy = Resources.Load<Transform>("pfEnemy");
        Transform enemyTranform = Instantiate(pfEnemy, pos, Quaternion.identity);

        Enemy enemy = enemyTranform.GetComponent<Enemy>();
        return enemy;//return if someone calls it they have something to do with

    }



    private Transform targetTransform;
    private float lookForTargetTimer;
    private float lookForTargetTimerMax = .2f;
    private Rigidbody2D rb2D;

    private HealthSystem healthSystem;


    void Start()
    {
        targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
        rb2D = GetComponent<Rigidbody2D>();
        healthSystem = GetComponent<HealthSystem>();
        lookForTargetTimer = Random.Range(0f, lookForTargetTimerMax);
        healthSystem.OnDied += HealthSystem_OnDied;


    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleTargeting();


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Building building = collision.gameObject.GetComponent<Building>();
        if (building != null)
        {
            Debug.Log("COLLSION WITH BUILDING");
            HealthSystem healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.Damage(10);
            Destroy(gameObject);
        }

    }

    private void HandleMovement()
    {
        if (targetTransform != null)
        {
            Vector3 moveDir = (targetTransform.position - transform.position).normalized;
            float moveSpeed = 6f;
            rb2D.linearVelocity = moveDir * moveSpeed;

        }
        else
            rb2D.linearVelocity = Vector2.zero;
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

    private void LookForTargets()
    {
        float targetMaxRadius = 10f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);


        foreach (Collider2D collider2D in collider2DArray)
        {
            Building building = collider2D.GetComponent<Building>();
            if (building != null)
            {
                if (targetTransform == null)
                {
                    targetTransform = building.transform;
                }
                else
                {
                    if (Vector3.Distance(transform.position, building.transform.position) < Vector3.Distance(transform.position, targetTransform.position))
                    {
                        //closer
                        targetTransform = building.transform;
                    }
                }
            }
        }

        if (targetTransform == null)
        {
            targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
        }

    }
}
