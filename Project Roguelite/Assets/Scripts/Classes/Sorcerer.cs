using UnityEngine;

public class Sorcerer : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded;
    public bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);

        if (moveHorizontal < 0 && facingRight || moveHorizontal > 0 && !facingRight)
        {
            Flip();
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
