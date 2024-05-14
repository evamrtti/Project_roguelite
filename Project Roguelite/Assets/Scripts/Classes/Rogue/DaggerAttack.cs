using System.Collections;
using UnityEngine;

public class DaggerAttack : MonoBehaviour
{
    private bool inContact = false;
    private GameObject enemy;
    private int damageAmount;
    public int minDamage;
    public int maxDamage;
    private bool canAttack = true;
    public float initialDelay = 0.5f;
    public float timeBetweenAttacks;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && inContact && canAttack)
        {
            StartCoroutine(AttackSequence());
        }
    }

    IEnumerator AttackSequence()
    {
        damageAmount = Random.Range(minDamage, maxDamage);

        var enemyHealth = enemy.GetComponent<EnemyHealth>();
        enemyHealth.TakeDamage(damageAmount);
        canAttack = false;
        yield return new WaitForSeconds(initialDelay);
        enemyHealth.TakeDamage(damageAmount);
        yield return new WaitForSeconds(timeBetweenAttacks);
        canAttack = true;
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
