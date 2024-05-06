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
        Vector3 playerPosition = player.transform.position;
        Vector3 newPosition = new Vector3(playerPosition.x, playerPosition.y + 3f, playerPosition.z);
        newWeapon.transform.localPosition = newPosition;
        newWeapon.transform.localRotation = Quaternion.identity;
    }
}
