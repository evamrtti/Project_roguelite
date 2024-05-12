using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private bool inContact = false;
    private GameObject player;
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

        if (inContact && canAttack)
        {
            var playerHealth = player.GetComponent<KnightHealth>();
            playerHealth.TakeDamage(damageAmount);
            canAttack = false;

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inContact = true;
            player = collision.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            inContact = false;
        }
    }
}