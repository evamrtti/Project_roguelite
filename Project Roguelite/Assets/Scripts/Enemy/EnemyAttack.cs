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
    private PlayerHealth playerHealth;

    void Update()
    {
        damageAmount = Random.Range(minDamage, maxDamage+1);
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
                playerHealth = player.GetComponent<PlayerHealth>();
                playerHealth.TakeDamage(damageAmount,0);
                Debug.Log("Flat damage : " + playerHealth.totalDamage + " Defense : " + playerHealth.defense + " Shield : " + playerHealth.shield);
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
