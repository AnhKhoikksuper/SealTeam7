using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator anim;
    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Nhận input trái / phải
        moveInput = Input.GetAxisRaw("Horizontal");

        // Gửi tốc độ cho Animator (dùng trị tuyệt đối)
        anim.SetFloat("MoveSpeed", Mathf.Abs(moveInput));

        // Lật mặt nhân vật
        if (moveInput != 0)
        {
            transform.localScale = new Vector3(
                Mathf.Sign(moveInput),
                1,
                1
            );
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }
}
