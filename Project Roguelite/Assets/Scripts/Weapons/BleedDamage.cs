using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedDamage : MonoBehaviour
{
    public float bleedingDamage = 35;
    public int requiredHits = 3;
    private float lastAttackTime;
    private bool inContact = false;
    private GameObject enemy;
    private float attackCooldown;
    private SwordAttack swordAttack;
    private DaggerAttack daggerAttack;
    private WandFire wandFire;

    private Dictionary<GameObject, int> bleedGauge = new Dictionary<GameObject, int>();

    void Update()
    {
        if (swordAttack == null)
        {
            swordAttack = GetComponent<SwordAttack>();
            if (swordAttack != null)
            {
                attackCooldown = swordAttack.timeBetweenAttacks;
                Debug.Log("Attack speed is " + attackCooldown);
            }
        }

        if (daggerAttack == null)
        {
            daggerAttack = GetComponent<DaggerAttack>();
            if (daggerAttack != null)
            {
                attackCooldown = daggerAttack.timeBetweenAttacks;
                Debug.Log("Attack speed is " + attackCooldown);
            }
        }

        if (wandFire == null)
        {
            wandFire = GetComponent<WandFire>();
            if (wandFire != null)
            {
                attackCooldown = wandFire.timeBetweenFiring;
                Debug.Log("Attack speed is " + attackCooldown);
            }
        }


        if (Input.GetMouseButtonDown(0) && inContact && PlayerCanAttack())
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

    public bool PlayerCanAttack()
    {
        if (swordAttack != null && swordAttack.canAttack)
        {
            return true;
        }

        if (daggerAttack != null && daggerAttack.canAttack)
        {
            return true;
        }

        if (wandFire != null && wandFire.canFire)
        {
            return true;
        }

        return false;
    }

    void ProcBleed(GameObject target)
    {
        bleedGauge[target]++;
        Debug.Log("Bleeding gauge = " + bleedGauge[target] + " for " + target.name);
        lastAttackTime = Time.time;

        if (bleedGauge[target] >= requiredHits)
        {
            Debug.Log("Bleed damage of : " + bleedingDamage + " to " + enemy.name);
            var enemyHealth = target.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(bleedingDamage);
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




