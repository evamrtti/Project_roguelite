using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedDamage : MonoBehaviour
{
    public float bleedingDamage = 35;
    public int requiredHits = 3;
    public float attackCooldown = 1.2f;
    private float lastAttackTime;


    private bool inContact = false;
    private GameObject enemy;


    private Dictionary<GameObject, int> bleedGauge = new Dictionary<GameObject, int>();

    void Update()
    {
        bool canAttack = Time.time - lastAttackTime >= attackCooldown;

        if (Input.GetMouseButtonDown(0) && inContact && canAttack)
        {
            if (!bleedGauge.ContainsKey(enemy))
            {
                bleedGauge[enemy] = 0;
            }

            if (bleedGauge.ContainsKey(enemy))
            {
                ProcBleed(enemy);
            }
        }
    } 

    void ProcBleed(GameObject target)
    {
        bleedGauge[target]++;
        Debug.Log("Gauge = " + bleedGauge[target] + " for " + target.name);
        lastAttackTime = Time.time;

        if (bleedGauge[target] >= requiredHits)
        {
            var enemyHealth = target.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(bleedingDamage);
                Debug.Log("Bleeding damage to " + target.name);
            }
            bleedGauge.Remove(target);
        }
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




