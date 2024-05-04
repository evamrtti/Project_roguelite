using UnityEngine;
using UnityEngine.InputSystem;

public class WandFire : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet;
    public bool canFire = true;
    private float timer;
    public float timeBetweenFiring;
    private bool isPickedUp = false;
    private CharacterController characterController;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        characterController = FindObjectOfType<CharacterController>();
    }

    void Update()
    {
        if (isPickedUp)
        {
            Fire();
        }
    }

    void Fire()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 shootingPos = transform.position + transform.right * 1f;

        Vector3 rotation = mousePos - transform.position;
        float rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        if (characterController.facingRight)
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
            Instantiate(bullet, shootingPos, Quaternion.identity);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPickedUp = true;
        }
    }
}
