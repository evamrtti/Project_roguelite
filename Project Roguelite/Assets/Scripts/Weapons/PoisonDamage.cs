using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDamage : MonoBehaviour
{
    public float poisonDamage = 10;
    public float poisonDuration = 3;
    private float poisonTimer = 0;
    private float cooldownTimer = 0;
    private float poisonDamageTimer = 0;
    public float cooldown = 5;
    private bool isPoisoned = false;
    private bool inContact = false;
    private bool isCooldownActive = false;
    private GameObject enemy;

    void Update()
    {
        if (!isCooldownActive && !isPoisoned)
        {
            if (Input.GetMouseButtonDown(0) && inContact && !isPoisoned)
            {
                ProcPoison();
                Debug.Log("Poison applied");
            }
        }
        if (isCooldownActive)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= cooldown)
            {
                isCooldownActive = false;
                cooldownTimer = 0;
            }
        }

        if (isPoisoned)
        {
            poisonTimer += Time.deltaTime;
            poisonDamageTimer += Time.deltaTime;

            if (poisonDamageTimer >=1)
            {
                ProcPoison();
                Debug.Log("Poison applied again");
                poisonDamageTimer = 0;
            }

            if (poisonTimer >= poisonDuration)
            {
                isPoisoned = false;
                ResetPoison();
            }
        }
    }

    void ProcPoison()
    {
        if (enemy != null)
        {
            var enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(poisonDamage);
                isPoisoned = true;
            }
        }
    }

    void ResetPoison()
    {
        poisonTimer = 0;
        poisonDamageTimer = 0;
        Debug.Log("No more poison");
        isCooldownActive = true;
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
