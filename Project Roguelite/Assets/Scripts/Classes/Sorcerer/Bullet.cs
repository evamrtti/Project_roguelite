using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject enemy;
    private float damageAmount;
    public int minDamage;
    public int maxDamage;

    void Update()
    {
        damageAmount = Random.Range(minDamage, maxDamage);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemy = collision.gameObject;
            var enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damageAmount);
            Destroy(gameObject); 
        }
    }
}
