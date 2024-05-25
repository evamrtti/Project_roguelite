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
    private bool playerCanAttack;

    private Dictionary<GameObject, int> bleedGauge = new Dictionary<GameObject, int>();

    void Update()
    {

        if (Input.GetMouseButtonDown(0) && inContact && playerCanAttack)
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

        CheckAttackScripts();
        Debug.Log(playerCanAttack);
    }

    void CheckAttackScripts()
    {
        
        if (swordAttack != null)
        {
            playerCanAttack = swordAttack.canAttack;
        }
        else
        {
            swordAttack = GetComponent<SwordAttack>();
        }

        if (daggerAttack != null)
        {
            playerCanAttack = daggerAttack.canAttack;
        }
        else
        {
            daggerAttack = GetComponent<DaggerAttack>();
        }

        if (wandFire != null)
        {
            playerCanAttack = wandFire.canFire;
        }
        else
        {
            wandFire = GetComponent<WandFire>();
        }
    }

    void ProcBleed(GameObject target)
    {
        if (!bleedGauge.ContainsKey(target))
        {
            bleedGauge[target] = 0;
        }

        bleedGauge[target]++;
        Debug.Log("Gauge = " + bleedGauge[target] + " for " + target.name);

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
