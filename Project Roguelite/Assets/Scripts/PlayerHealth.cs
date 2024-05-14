using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float currentPlayerHealth;
    public float maxPlayerHealth;
    public float defense;
    public int minPlayerDefense;
    public int maxPlayerDefense;

    void Start()
    {
        currentPlayerHealth = maxPlayerHealth;
    }

    void Update()
    {
        defense = Random.Range(minPlayerDefense, maxPlayerDefense);
    }

    public void TakeDamage(float damageAmount)
    {
        if (defense > damageAmount)
        {
            defense = damageAmount;
        }

        currentPlayerHealth -= damageAmount - defense;

        if (currentPlayerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
