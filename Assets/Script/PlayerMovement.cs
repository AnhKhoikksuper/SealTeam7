using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;

    [Header("Jump")]
    public float jumpForce = 30f;
    public float jumpCutMultiplier = 0.5f;
    public float maxFallSpeed = -25f;
    [Header("Coyote & Buffer")]
    public float coyoteTime = 0.15f;
    public float jumpBufferTime = 0.15f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.15f;
    public LayerMask groundLayer;
    [Header("Audio")]
    public AudioClip footstepClip;
    public float footstepInterval = 0.4f;
    public AudioClip jumpClip;
    private AudioSource audioSource;
    private float footstepTimer;


    [Header("Control")]
    public bool canControl = true; // ⭐ BIẾN KHÓA ĐIỀU KHIỂN

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
        sr = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        // ❌ Nếu bị khóa điều khiển
        if (!canControl)
        {
            moveInput = 0;
            anim.SetFloat("MoveSpeed", 0);
            return;
        }

        // Input
        moveInput = Input.GetAxisRaw("Horizontal");

        // Animation
        anim.SetFloat("MoveSpeed", Mathf.Abs(moveInput));

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

            // ⭐ PHÁT ÂM THANH NHẢY
            if (jumpClip != null)
                audioSource.PlayOneShot(jumpClip);

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
        HandleFootstepSound();

    }

    void FixedUpdate()
    {
        if (!canControl)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }
        if (rb.linearVelocity.y < maxFallSpeed)
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                maxFallSpeed
            );
        rb.linearVelocity = new Vector2(
            moveInput * moveSpeed,
            rb.linearVelocity.y
        );
    }

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
        sr.flipX = false;
    }

    void FaceLeft()
    {
        isFacingRight = false;
        sr.flipX = true;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    void HandleFootstepSound()
    {
        if (!isGrounded || Mathf.Abs(moveInput) < 0.1f)
        {
            footstepTimer = 0;
            return;
        }

        footstepTimer -= Time.deltaTime;

        if (footstepTimer <= 0)
        {
            if (footstepClip != null)
                audioSource.PlayOneShot(footstepClip);

            footstepTimer = footstepInterval;
        }
    }

}
