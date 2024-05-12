using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private bool inContact = false;
    private GameObject enemy;
    private int hitCounter = 0;
    public int requiredHits = 6;
    private bool isStunned = false;
    private float stunTimer = 0;
    public float stunDuration = 3;
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
