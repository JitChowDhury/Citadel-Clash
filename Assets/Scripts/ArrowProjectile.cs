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

    private void Update()
    {
        Vector3 moveDir = (targetEnemy.transform.position - transform.position).normalized;

        float moveSpeed = 20f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
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
        }
    }
}
