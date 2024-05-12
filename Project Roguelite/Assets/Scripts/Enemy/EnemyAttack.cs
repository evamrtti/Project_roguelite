using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private bool inContact = false;
    private GameObject player;
    private float damageAmount;
    public int minDamage;
    public int maxDamage;
    private bool canAttack = true;
    private float timeBeforeAttacking;
    public float timeBetweenAttacks;

    void Update()
    {
        damageAmount = Random.Range(minDamage, maxDamage);
        Attack();
    }

    void Attack()
    {
        if (!canAttack)
        {
            timeBeforeAttacking += Time.deltaTime;
            if (timeBeforeAttacking > timeBetweenAttacks)
            {
                canAttack = true;
                timeBeforeAttacking = 0;
            }
        }

        if (inContact && canAttack)
        {
            if (player != null)
            {
                if (player.GetComponent<KnightHealth>() != null)
                {
                    player.GetComponent<KnightHealth>().TakeDamage(damageAmount);
                }
                else if (player.GetComponent<RogueHealth>() != null)
                {
                    player.GetComponent<RogueHealth>().TakeDamage(damageAmount);
                }
                else if (player.GetComponent<SorcererHealth>() != null)
                {
                    player.GetComponent<SorcererHealth>().TakeDamage(damageAmount);
                }

                canAttack = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inContact = true;
            player = collision.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            inContact = false;
        }
    }
}
