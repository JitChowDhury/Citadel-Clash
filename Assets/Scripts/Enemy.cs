using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Enemy : MonoBehaviour
{
    private Transform targetTransform;
    private Rigidbody2D rb2D;


    void Start()
    {
        targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = (targetTransform.position - transform.position).normalized;
        float moveSpeed = 6f;
        rb2D.linearVelocity = moveDir * moveSpeed;
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
}
