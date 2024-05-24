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
        ShieldDefense shieldDefense = GameObject.FindObjectOfType<ShieldDefense>();

        if (!canAttack)
        {
            timeBeforeAttacking += Time.deltaTime;
            if (timeBeforeAttacking > timeBetweenAttacks)
            {
                canAttack = true;
                timeBeforeAttacking = 0;
            }
        }

        if (Input.GetMouseButtonDown(0) && inContact && canAttack && shieldDefense != null && !shieldDefense.isDefending)
        {
            damageAmount = Random.Range(minDamage, maxDamage+1);
            Debug.Log("Sword attack damage : " + damageAmount);
            Attack();
        }
    }

    void Attack()
    {
            var enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damageAmount);
            canAttack = false;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            inContact = true;
            enemy = collision.gameObject; 
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == enemy)
        {
            inContact = false; 
        }
    }
}