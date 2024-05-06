using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    private bool inContact = false;
    private GameObject enemy;
    private float damageAmount;
    public int minDamage;
    public int maxDamage;
    private bool canAttack = true;
    private float timeBeforeAttacking;
    public float timeBetweenAttacks;

    void Update()
    {
        damageAmount = Random.Range(minDamage, maxDamage);    
        Attack();
    }

    void Attack()
    {
        if (!canAttack)
        {
            timeBeforeAttacking += Time.deltaTime;
            if (timeBeforeAttacking > timeBetweenAttacks)
            {
                canAttack = true;
                timeBeforeAttacking = 0;
            }
        }

        if (Input.GetMouseButtonDown(0) && inContact && canAttack)
        {
            var enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damageAmount);
            canAttack = false;

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            inContact = true;
            enemy = collision.gameObject; 
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == enemy)
        {
            inContact = false; 
        }
    }
}