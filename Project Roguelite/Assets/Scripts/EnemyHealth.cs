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
        defense = Random.Range(minDefense, maxDefense);
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount - defense;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
