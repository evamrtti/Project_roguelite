using UnityEngine;

public class WeaponChange : MonoBehaviour
{
    public GameObject newWeaponPrefab; 

    private void OnTriggerEnter2D(Collider2D weapon)
    {
        if (weapon.CompareTag("Player"))
        {
  
            GameObject player = weapon.gameObject;


            RemoveOldWeapon(player);

            InstantiateNewWeapon(player);

            Destroy(gameObject);
        }
    }

    private void RemoveOldWeapon(GameObject player)
    {

        foreach (Transform child in player.transform)
        {

            if (child.CompareTag("Weapon"))
            {

                Destroy(child.gameObject);
            }
        }
    }

    private void InstantiateNewWeapon(GameObject player)
    {
        GameObject newWeapon = Instantiate(newWeaponPrefab, player.transform);


        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.transform.localRotation = Quaternion.identity;
    }
}
