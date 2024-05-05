using UnityEngine;

public class BetterSword : MonoBehaviour
{
    public float damageIncrease = 5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SwordAttack sword = other.GetComponentInChildren<SwordAttack>();
            if (sword != null)
            {
                sword.damageAmount += damageIncrease;
            }

            Destroy(gameObject);
        }
    }
}