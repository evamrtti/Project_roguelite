using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDamage : MonoBehaviour
{
    public float poisonDamage = 10;
    public float poisonDuration = 3;
    public float cooldown = 5;
    private float attackCooldown;
    private SwordAttack swordAttack;
    private DaggerAttack daggerAttack;
    private WandFire wandFire;

    private bool inContact = false;
    private GameObject enemy;
    private float lastAttackTime;

    private Dictionary<GameObject, float> poisonEndTimes = new Dictionary<GameObject, float>();
    private Dictionary<GameObject, float> cooldownEndTimes = new Dictionary<GameObject, float>();

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

        bool canAttack = Time.time - lastAttackTime >= attackCooldown;

        if (Input.GetMouseButtonDown(0) && canAttack && inContact)
        {
            if (!IsPoisoned(enemy) && !IsOnCooldown(enemy))
            {
                Debug.Log(enemy.name + " is poisoned");
                ApplyPoison(enemy);

            }
        }

  
        UpdatePoisonStates();
        UpdateCooldownStates();
    }

    void ApplyPoison(GameObject target)
    {
        float endTime = Time.time + poisonDuration;
        poisonEndTimes[target] = endTime;
        cooldownEndTimes[target] = endTime + cooldown;
        lastAttackTime = Time.time;
        StartCoroutine(ApplyPoisonCoroutine(target, endTime));
    }

    IEnumerator ApplyPoisonCoroutine(GameObject target, float endTime)
    {
        while (Time.time < endTime)
        {
            var enemyHealth = target.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                Debug.Log("Poison damage of : " + poisonDamage + " to " + target.name);
                enemyHealth.TakeDamage(poisonDamage);
            }
            yield return new WaitForSeconds(1.0f); 
        }
    }

    void UpdatePoisonStates()
    {
        List<GameObject> keysToRemove = new List<GameObject>();

        foreach (GameObject target in poisonEndTimes.Keys)
        {
            if (Time.time >= poisonEndTimes[target])
            {
                keysToRemove.Add(target);
                Debug.Log("Poison removed from " + target.name);
            }
        }

        foreach (GameObject key in keysToRemove)
        {
            poisonEndTimes.Remove(key);
        }
    }

    void UpdateCooldownStates()
    {
        List<GameObject> keysToRemove = new List<GameObject>();

        foreach (GameObject target in cooldownEndTimes.Keys)
        {
            if (target == null) continue; 

            if (Time.time >= cooldownEndTimes[target])
            {
                keysToRemove.Add(target);
                Debug.Log("Poison cooldown ended for " + target.name);
            }
        }

        foreach (GameObject key in keysToRemove)
        {
            cooldownEndTimes.Remove(key);
        }
    }


    bool IsPoisoned(GameObject target)
    {
        return poisonEndTimes.ContainsKey(target);
    }

    bool IsOnCooldown(GameObject target)
    {
        return cooldownEndTimes.ContainsKey(target);
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
