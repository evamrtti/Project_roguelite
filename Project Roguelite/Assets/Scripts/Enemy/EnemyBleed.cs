using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBleed : MonoBehaviour
{
    public float bleedingDamage = 35;
    public int requiredHits = 3;
    private bool canAttack = true;
    private bool inContact = false;
    private GameObject player;
    private float lastAttackTime;
    private float attackCooldown;
    private int hitCounter = 0;
    private PlayerHealth playerHealth;
    private EnemyAttack enemyAttack;


    void Update()
    {
        BleedGauge();

        if (enemyAttack == null)
        {
            enemyAttack = GetComponent<EnemyAttack>();
            if (enemyAttack != null)
            {
                attackCooldown = enemyAttack.timeBetweenAttacks;
                Debug.Log("Enemy attack speed is " + attackCooldown);
            }
        }



            if (hitCounter >= requiredHits)
        {
            ProcBleed();
        }
    }


    void BleedGauge()
    {
        if (!canAttack)
        {
            lastAttackTime += Time.deltaTime;
            if (lastAttackTime > attackCooldown)
            {
                canAttack = true;
                lastAttackTime = 0;
            }
        }

        if (canAttack && inContact)
        {
            hitCounter++;
            Debug.Log("Player gauge = " + hitCounter);
            canAttack = false;
        }
    }

    void ProcBleed()
    {
        Debug.Log("Bleeding started!");

        playerHealth = player.GetComponent<PlayerHealth>();
        if (player != null)
        {
            playerHealth.TakeDamage(0, bleedingDamage);
            Debug.Log("Bleed damage : " + playerHealth.totalDamage + " Defense : " + playerHealth.defense + " Shield : " + playerHealth.shield);
            canAttack = false;
        }

        hitCounter = 0;
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
