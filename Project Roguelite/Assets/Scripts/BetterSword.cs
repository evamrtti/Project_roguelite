using UnityEngine;

public class BetterSword : MonoBehaviour
{
    public int damageDif;
    public int timeDif;
    public int speedDif;
    public int defenseDif;
    public int HPDif;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SwordAttack sword = other.GetComponentInChildren<SwordAttack>();
            KnightMovement speed = other.GetComponentInChildren<KnightMovement>();
            KnightHealth health = other.GetComponentInChildren<KnightHealth>();

            if (sword != null)
            {
                sword.minDamage += damageDif;
                sword.maxDamage += damageDif;
                sword.timeBetweenAttacks += timeDif;
                speed.moveSpeed += speedDif;
                health.minPlayerDefense += defenseDif;
                health.maxPlayerDefense += defenseDif;
                health.maxPlayerHealth += HPDif;
                health.currentPlayerHealth += HPDif;

            }

            Destroy(gameObject);
        }
    }
}