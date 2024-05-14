using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBleed : MonoBehaviour
{
    public float bleedingDamage = 35;
    public int requiredHits = 3;
    private int hitCounter = 0;
    private bool hasBled = false;
    private bool canAttack = true;
    private bool inContact = false;
    private GameObject player;
    private float timeBeforeAttacking;
    public float timeBetweenAttacks;

    void Update()
    {
        BleedGauge();

        if (hasBled)
        {
            ResetBleeding();
        }
        else if (hitCounter >= requiredHits)
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
            Debug.Log("Enemy gauge growing!");
            canAttack = false;
        }
    }

    void ProcBleed()
    {
        Debug.Log("Bleeding on player started!");
        hasBled = true;
    }


    void ResetBleeding()
    {
        hitCounter = 0;
        hasBled = false;
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




