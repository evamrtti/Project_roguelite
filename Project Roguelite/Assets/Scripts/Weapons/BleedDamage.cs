using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedSword : MonoBehaviour
{
    public float bleedingDamage = 35; 
    public int requiredHits = 3; 

    private int hitCounter = 0;
    private bool hasBled = false;

    private bool inContact = false;
    private GameObject enemy;

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

        if (Input.GetMouseButtonDown(0) && inContact)
        {
            hitCounter++;
            Debug.Log("Gauge growing!");
        }
    }

    void ProcBleed()
    {
        if (enemy != null)
        {
            var enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(bleedingDamage);
                Debug.Log("Bleeding started!");
                hasBled = true;
            }
        }
    }

    void ResetBleeding()
    {
        hitCounter = 0;
        hasBled = false;
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




