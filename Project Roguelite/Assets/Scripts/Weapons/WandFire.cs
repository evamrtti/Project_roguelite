using UnityEngine;
using UnityEngine.InputSystem;

public class WandFire : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bulletPrefab;
    public bool canFire = true;
    private float timer;
    public float timeBetweenFiring;
    private Sorcerer sorcerer;
    public float force;
    public float rotationBullet;


    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        sorcerer = FindObjectOfType<Sorcerer>();
    }

    void Update()
    {
       
            Fire();
        
    }

    void Fire()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 shootingPos = transform.position + transform.right * 1f;

        Vector3 rotation = mousePos - transform.position;
        float rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        if (sorcerer.facingRight)
        {
            rotz = Mathf.Clamp(rotz, -90f, 90f);
        }
        else
        {
            if (rotz < 0)
            {
                rotz = Mathf.Clamp(rotz, -270f, -90f);
            }
            else
            {
                rotz = Mathf.Clamp(rotz, 90f, 270f);
            }
        }

        transform.rotation = Quaternion.Euler(0f, 0f, rotz);

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            GameObject bullet = Instantiate(bulletPrefab, shootingPos, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector3 direction = mousePos - transform.position;
            rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
            rb.angularVelocity = rotationBullet;
        }
    }

}
