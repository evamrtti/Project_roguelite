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
    public int shield;
    public float flatDamage;
    public float totalDamage;

    void Start()
    {
        currentPlayerHealth = maxPlayerHealth;
    }

    void Update()
    {
        defense = Random.Range(minPlayerDefense, maxPlayerDefense+1);
    }

    public void TakeDamage(float damageAmount, float fxDamage)
    {
        flatDamage = Mathf.Max(damageAmount - shield, 0);
        if (fxDamage == 0)
        {
            totalDamage = Mathf.Max(flatDamage - defense, 0);
            currentPlayerHealth -= totalDamage;
        }

        else
        {
            totalDamage = Mathf.Max(fxDamage - defense, 0);
            currentPlayerHealth -= totalDamage;
        }

        if (currentPlayerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
