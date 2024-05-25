using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStun : MonoBehaviour
{
    private bool inContact = false;
    private GameObject player;
    private int hitCounter = 0;
    public float stunDuration = 3;
    public int requiredHits = 6;
    private bool isStunned = false;
    private float stunTimer = 0;
    private float lastAttackTime;
    private float attackCooldown;
    private bool canStun = true;
    private bool canAttack = true;
    private MonoBehaviour playerMovement;
    private float originalMovement;
    private MonoBehaviour playerAttack;
    private bool originalPlayerCanAttack;
    private EnemyAttack enemyAttack;

    void Update()
    {
        if (enemyAttack == null)
        {
            enemyAttack = GetComponent<EnemyAttack>();
            if (enemyAttack != null)
            {
                attackCooldown = enemyAttack.timeBetweenAttacks;
            }
        }

        StunGauge();

        if (isStunned)
        {
            stunTimer += Time.deltaTime;
            if (stunTimer >= stunDuration)
            {
                ResetStun();
            }
            else
            {
                DisablePlayerAttacks();
                DisablePlayerMovement();
            }
        }
        else if (hitCounter >= requiredHits && canStun)
        {
            Debug.Log("Stunned!");
            Stun();
        }
    }

    void StunGauge()
    {
        if (!canAttack)
        {
            lastAttackTime += Time.deltaTime;
            if (lastAttackTime > attackCooldown)
            {
                canAttack = true;
                lastAttackTime = 0;
            }
        }

        if (inContact && canAttack && !isStunned)
        {
            hitCounter++;
            Debug.Log("Player stun gauge growing! Hit Counter: " + hitCounter);
            canAttack = false;
        }
    }

    void Stun()
    {
        if (player != null)
        {
            AssignPlayerMovementComponent();
            AssignPlayerAttackComponent();
            isStunned = true;
            canStun = false;
            stunTimer = 0;
        }
    }

    void ResetStun()
    {
        stunTimer = 0;
        hitCounter = 0;
        isStunned = false;
        canStun = true;
        Debug.Log("Not stunned anymore");

        if (player != null && playerMovement != null)
        {
            if (playerMovement is KnightMovement knightMovement)
            {
                knightMovement.moveSpeed = originalMovement;
            }
            else if (playerMovement is RogueMovement rogueMovement)
            {
                rogueMovement.moveSpeed = originalMovement;
            }
            else if (playerMovement is SorcererMovement sorcererMovement)
            {
                sorcererMovement.moveSpeed = originalMovement;
            }
        }

        if (playerAttack != null && !isStunned)
        {
            if (playerAttack is SwordAttack swordAttack)
            {
                swordAttack.canAttack = originalPlayerCanAttack;
            }
            else if (playerAttack is DaggerAttack daggerAttack)
            {
                daggerAttack.canAttack = originalPlayerCanAttack;
            }
            else if (playerAttack is WandFire wandFire)
            {
                wandFire.canFire = originalPlayerCanAttack;
            }
        }
    }

    void DisablePlayerAttacks()
    {
        if (playerAttack != null)
        {
            if (playerAttack is SwordAttack swordAttack)
            {
                swordAttack.canAttack = false;
            }
            else if (playerAttack is DaggerAttack daggerAttack)
            {
                daggerAttack.canAttack = false;
            }
            else if (playerAttack is WandFire wandFire)
            {
                wandFire.canFire = false;
            }
        }
    }

    void DisablePlayerMovement()
    {
        if (playerMovement != null)
        {
            if (playerMovement is KnightMovement knightMovement)
            {
                knightMovement.moveSpeed = 0;
            }
            else if (playerMovement is RogueMovement rogueMovement)
            {
                rogueMovement.moveSpeed = 0;
            }
            else if (playerMovement is SorcererMovement sorcererMovement)
            {
                sorcererMovement.moveSpeed = 0;
            }
        }
    }


    void AssignPlayerMovementComponent()
    {
        if (playerMovement == null && player != null)
        {
            playerMovement = player.GetComponent<KnightMovement>() as MonoBehaviour ??
                             player.GetComponent<RogueMovement>() as MonoBehaviour ??
                             player.GetComponent<SorcererMovement>() as MonoBehaviour;

            if (playerMovement != null)
            {
                if (playerMovement is KnightMovement knightMovement)
                {
                    originalMovement = knightMovement.moveSpeed;
                }
                else if (playerMovement is RogueMovement rogueMovement)
                {
                    originalMovement = rogueMovement.moveSpeed;
                }
                else if (playerMovement is SorcererMovement sorcererMovement)
                {
                    originalMovement = sorcererMovement.moveSpeed;
                }
            }
        }
    }

    void AssignPlayerAttackComponent()
    {
        if (playerAttack == null && player != null)
        {
            playerAttack = player.GetComponentInChildren<SwordAttack>() as MonoBehaviour ??
                           player.GetComponentInChildren<DaggerAttack>() as MonoBehaviour ??
                           player.GetComponentInChildren<WandFire>() as MonoBehaviour;

            if (playerAttack != null)
            {
                if (playerAttack is SwordAttack swordAttack)
                {
                    originalPlayerCanAttack = swordAttack.canAttack;
                }
                else if (playerAttack is DaggerAttack daggerAttack)
                {
                    originalPlayerCanAttack = daggerAttack.canAttack;
                }
                else if (playerAttack is WandFire wandFire)
                {
                    originalPlayerCanAttack = wandFire.canFire;
                }
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
            player = null;
            playerMovement = null;
            playerAttack = null;
        }
    }
}
