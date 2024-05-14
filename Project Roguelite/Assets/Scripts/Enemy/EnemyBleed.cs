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
    private float timeBeforeAttacking;
    public float timeBetweenAttacks;
    private int hitCounter = 0;
    private PlayerHealth playerHealth;


    void Update()
    {
        BleedGauge();

        if (hitCounter >= requiredHits)
        {
            ProcBleed();
        }
    }


    void BleedGauge()
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

        if (player!= null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(bleedingDamage);
            Debug.Log("Player takes damage: " + bleedingDamage);
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
