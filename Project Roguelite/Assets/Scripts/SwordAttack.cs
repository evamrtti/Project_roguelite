using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    private bool reachedEnemy = false;
    private bool canAttack = false;
    private GameObject enemy;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 
        transform.SetParent(player.transform);
        transform.localPosition = new Vector3(0.5f, 0, 0);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack ) 
        {
            Attack();
        }
    }

    void Attack()
    {
        if (reachedEnemy)
        {
            Destroy(enemy);
            enemy = null;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            reachedEnemy = true;
            canAttack = true;
            enemy = collision.gameObject; 
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == enemy)
        {
            canAttack = false; 
        }
    }
}