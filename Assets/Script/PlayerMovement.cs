using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;

    [Header("Jump")]
    public float jumpForce = 30f;
    public float jumpCutMultiplier = 0.5f;

    [Header("Coyote & Buffer")]
    public float coyoteTime = 0.15f;
    public float jumpBufferTime = 0.15f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.15f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private float moveInput;
    private bool isFacingRight = true;

    private bool isGrounded;
    private float coyoteTimer;
    private float jumpBufferTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>(); // 👈 LẤY SPRITE
    }

    void Update()
    {
        // Input
        moveInput = Input.GetAxisRaw("Horizontal");

        // ===== Animation: MoveSpeed =====
        anim.SetFloat("MoveSpeed", Mathf.Abs(moveInput));

        // Flip mặt (ĐÃ FIX)
        HandleFlip();

        // Ground check
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        // Coyote time
        if (isGrounded)
            coyoteTimer = coyoteTime;
        else
            coyoteTimer -= Time.deltaTime;

        // Jump buffer
        if (Input.GetKeyDown(KeyCode.Space))
            jumpBufferTimer = jumpBufferTime;
        else
            jumpBufferTimer -= Time.deltaTime;

        // Jump
        if (jumpBufferTimer > 0 && coyoteTimer > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpBufferTimer = 0;
            coyoteTimer = 0;
        }

        // Jump cut
        if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                rb.linearVelocity.y * jumpCutMultiplier
            );
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            moveInput * moveSpeed,
            rb.linearVelocity.y
        );
    }

    // ======================
    // Flip nhân vật (KHÔNG XOAY ROTATION)
    // ======================
    void HandleFlip()
    {
        if (moveInput > 0 && !isFacingRight)
            FaceRight();
        else if (moveInput < 0 && isFacingRight)
            FaceLeft();
    }

    void FaceRight()
    {
        isFacingRight = true;
        sr.flipX = false; // 👈 quay phải
    }

    void FaceLeft()
    {
        isFacingRight = false;
        sr.flipX = true; // 👈 quay trái
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
