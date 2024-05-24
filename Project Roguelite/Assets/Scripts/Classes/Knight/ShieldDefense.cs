using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDefense : MonoBehaviour
{
    private bool inContact = false;
    private GameObject enemy;
    private PlayerHealth playerShield;
    public int shieldDif;
    public bool isDefending = false;
    private int originalShield;

    void Update()
    {
        if (!isDefending)
        {
            playerShield = GetComponentInParent<PlayerHealth>();
            originalShield = playerShield.shield;
        }

        if (inContact && Input.GetMouseButtonDown(1))
        {
            Defend();
        }

        if (!Input.GetMouseButton(1) && isDefending)
        {
            ResetDefense();
        }
    }

    public void Defend()
    {   
        isDefending = true;
        playerShield.shield+= shieldDif;
    }

    void ResetDefense()
    {
        isDefending = false;
        playerShield.shield = originalShield;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            inContact = true;
            enemy = collision.gameObject;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == enemy)
        {
            inContact = false;
        }
    }
}
