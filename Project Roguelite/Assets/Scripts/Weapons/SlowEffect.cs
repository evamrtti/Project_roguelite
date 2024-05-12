using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEffect : MonoBehaviour
{
    private bool inContact = false;
    private GameObject enemy;
    private int hitCounter = 0;
    public float slowDuration = 3;
    public int requiredHits = 6;
    private bool isSlowed = false;
    private float slowTimer = 0;
    private bool canSlow = true;

    void Update()
    {
        SlowGauge();

        if (isSlowed)
        {
            slowTimer += Time.deltaTime;
            if (slowTimer >= slowDuration)
            {
                isSlowed = false;
                ResetSlow();
            }
        }
        else if (hitCounter >= requiredHits && canSlow)
        {
            Slow();
        }
    }

    void SlowGauge()
    {
        if (Input.GetMouseButtonDown(0) && inContact)
        {
            hitCounter++;
            Debug.Log("Gauge growing!");
        }
    }

    void Slow()
    {
        if (enemy != null)
        {
            Debug.Log("Slowed!");
            isSlowed = true;
            canSlow = false;
        }
    }

    void ResetSlow()
    {
        slowTimer = 0;
        hitCounter = 0;
        isSlowed= false;
        canSlow = true;
        Debug.Log("Not slowed anymore");
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
