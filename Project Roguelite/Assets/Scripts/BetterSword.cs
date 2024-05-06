using UnityEngine;

public class BetterSword : MonoBehaviour
{
    public int damageIncrease = 5;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SwordAttack sword = other.GetComponentInChildren<SwordAttack>();
            if (sword != null)
            {
                sword.minDamage += damageIncrease;
                sword.maxDamage += damageIncrease;
            }

            Destroy(gameObject);
        }
    }
}