using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    private float defense;
    public int minDefense;
    public int maxDefense;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        defense = Random.Range(minDefense, maxDefense+1);
    }

    public void TakeDamage(float damageAmount)
    {
        if (defense > damageAmount)
        {
            defense = damageAmount;
        }
        float damageTaken = damageAmount - defense;
        currentHealth -= damageTaken;
        Debug.Log("Defense : " + defense + " Damage taken : " + damageTaken);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
