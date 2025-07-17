using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{

    public static ArrowProjectile Create(Vector3 pos, Enemy enemy)
    {
        Transform pfArrowProjectile = Resources.Load<Transform>("pfArrowProjectile");
        Transform arrowTranform = Instantiate(pfArrowProjectile, pos, Quaternion.identity);

        ArrowProjectile arrowProjectile = arrowTranform.GetComponent<ArrowProjectile>();
        arrowProjectile.SetTarget(enemy);
        return arrowProjectile;//return if someone calls it they have something to do with

    }

    private Enemy targetEnemy;
    private Vector3 lastMoveDir;

    private float timeToDie = 2f;

    private void Update()
    {
        Vector3 moveDir;
        if (targetEnemy != null)
        {
            moveDir = (targetEnemy.transform.position - transform.position).normalized;
            lastMoveDir = moveDir;
        }
        else
        {
            moveDir = lastMoveDir;
        }

        float moveSpeed = 20f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(moveDir));

        timeToDie -= Time.deltaTime;
        if (timeToDie < 0f)
        {
            Destroy(gameObject);
        }
    }

    private void SetTarget(Enemy targetEnemy)
    {
        this.targetEnemy = targetEnemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            //hit an enemy!
            Destroy(gameObject);
            int damageAmount = 10;
            enemy.GetComponent<HealthSystem>().Damage(damageAmount);
        }
    }
}
