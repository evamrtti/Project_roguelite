using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoison : MonoBehaviour
{
    public float poisonDamage;
    public float poisonDuration;
    public float cooldown;
    private float attackCooldown;
    private float lastAttackTime;
    private bool inContact = false;
    private bool isOnCooldown = false;
    private bool isPoisoned = false;
    private GameObject player;
    private PlayerHealth playerHealth;
    private EnemyAttack enemyAttack;
    private float endCooldownTime;
    private float endTime;


    void Update()
    {
        if (enemyAttack == null)
        {
            enemyAttack = GetComponent<EnemyAttack>();
            if (enemyAttack != null)
            {
                attackCooldown = enemyAttack.timeBetweenAttacks;
                Debug.Log("Enemy attack speed is " + attackCooldown);
            }
        }

        bool canAttack = Time.time - lastAttackTime >= attackCooldown;

        if (canAttack && inContact && !isOnCooldown && !isPoisoned)
        {
            isPoisoned=true;
            Debug.Log("Player is poisoned");
            ApplyPoison();
            lastAttackTime = Time.time;
        }

        if (Time.time > endCooldownTime)
        {
            isOnCooldown = false;
        }
    }

    void ApplyPoison()
    {
        endTime = Time.time + poisonDuration;
        endCooldownTime = endTime + cooldown;
        StartCoroutine(ApplyPoisonCoroutine(endTime));
        isOnCooldown = true;
    }

    IEnumerator ApplyPoisonCoroutine(float endTime)
    {
        while (Time.time < endTime)
        {
            if (player != null)
            {
                playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(0, poisonDamage);
                    Debug.Log("Poison damage to player : " + playerHealth.totalDamage + " Defense : " + playerHealth.defense + " Shield : " + playerHealth.shield);

                }
            }
            yield return new WaitForSeconds(1.0f);
        }

        isPoisoned = false;
        Debug.Log("Player not poisoned anymore");

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
