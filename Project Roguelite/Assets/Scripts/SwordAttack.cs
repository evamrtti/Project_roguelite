using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    private bool reachedEnemy = false;
    private bool canAttack = false;
    private GameObject enemy;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack ) 
        {
            Attack();
        }
    }

    void Attack()
    {
        if (reachedEnemy)
        {
            Destroy(enemy);
            enemy = null;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            reachedEnemy = true;
            canAttack = true;
            enemy = collision.gameObject; 
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == enemy)
        {
            canAttack = false; 
        }
    }
}