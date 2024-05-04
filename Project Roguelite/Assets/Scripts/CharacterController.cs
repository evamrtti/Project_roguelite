using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded;
    private bool hasJumped;
    private bool hasDoubleJumped;

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
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !hasJumped && !hasDoubleJumped)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            hasJumped = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && hasJumped && !hasDoubleJumped)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f); // Reset vertical velocity before double jump
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            hasDoubleJumped = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            hasJumped = false;
            hasDoubleJumped = false;
        }
    }
}
