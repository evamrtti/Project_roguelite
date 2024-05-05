using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttach : MonoBehaviour 
{
    private GameObject weapon;

    void Start()
    {
        Attach();
    }

    void Attach()
    {
        weapon = GameObject.FindGameObjectWithTag("Weapon");
        weapon.transform.SetParent(transform);
        weapon.transform.localPosition = new Vector3(0.5f, 0, 0);
        weapon.transform.localRotation = Quaternion.Euler(0, 0, 20f);
    }
}
