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
    private bool playerCanAttack;
    private bool inContact = false;
    private GameObject enemy;

    private Dictionary<GameObject, float> poisonEndTimes = new Dictionary<GameObject, float>();
    private Dictionary<GameObject, float> cooldownEndTimes = new Dictionary<GameObject, float>();

    void Update()
    {
       
        if (Input.GetMouseButtonDown(0) && playerCanAttack && inContact)
        {
            if (!IsPoisoned(enemy) && !IsOnCooldown(enemy))
            {
                Debug.Log(enemy.name + " is poisoned");
                ApplyPoison(enemy);

            }
        }

        CheckAttackScripts();
        UpdatePoisonStates();
        UpdateCooldownStates();
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

    void ApplyPoison(GameObject target)
    {
        float endTime = Time.time + poisonDuration;
        poisonEndTimes[target] = endTime;
        cooldownEndTimes[target] = endTime + cooldown;
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
