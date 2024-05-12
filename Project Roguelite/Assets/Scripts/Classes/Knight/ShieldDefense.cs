using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDefense : MonoBehaviour
{
    private bool inContact = false;
    private GameObject enemy;
    private KnightHealth playerDefense;
    public int defenseDif;
    private int originalMinDefense;
    private int originalMaxDefense;
    public bool isDefending = false;

    void Update()
    {
        if (!isDefending)
        {
            playerDefense = GetComponentInParent<KnightHealth>();
            originalMinDefense = playerDefense.minPlayerDefense;
            originalMaxDefense = playerDefense.maxPlayerDefense;
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
        playerDefense.minPlayerDefense += defenseDif;
        playerDefense.maxPlayerDefense += defenseDif;
    }

    void ResetDefense()
    {
        isDefending = false;
        playerDefense.minPlayerDefense = originalMinDefense;
        playerDefense.maxPlayerDefense = originalMaxDefense;
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
