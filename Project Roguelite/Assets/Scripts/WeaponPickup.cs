using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    private GameObject player;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PickUp();
        }
    }

    void PickUp()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.SetParent(player.transform);
        transform.localPosition = new Vector3(0.5f, 0, 0);
        transform.localRotation = Quaternion.Euler(0, 0, 20f);
    }
}
