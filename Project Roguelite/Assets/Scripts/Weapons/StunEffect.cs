using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEffect : MonoBehaviour
{
    private bool inContact = false;
    private GameObject enemy;
    private int hitCounter = 0;
    public float stunDuration = 3;
    public int requiredHits = 6;
    private bool isStunned = false;
    private float stunTimer = 0;
    private bool canStun = true;

    void Update()
    {
        StunGauge();

        if (isStunned)
        {
            stunTimer += Time.deltaTime;
            if (stunTimer >= stunDuration)
            {
                isStunned = false;
                ResetStun();
            }
        }
        else if (hitCounter >= requiredHits && canStun)
        {
            Stun();
        }
    }

    void StunGauge()
    {
        if (Input.GetMouseButtonDown(0) && inContact)
        {
            hitCounter++;
            Debug.Log("Gauge growing!");
        }
    }

    void Stun()
    {
        if (enemy != null)
        {
            Debug.Log("Stunned!");
            isStunned = true;
            canStun = false;
        }
    }

    void ResetStun()
    {
        stunTimer = 0;
        hitCounter = 0;
        isStunned= false;
        canStun = true;
        Debug.Log("Not stunned anymore");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            inContact = true;
            enemy = collision.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == enemy)
        {
            inContact = false;
        }
    }
}
