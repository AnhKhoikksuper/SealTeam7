using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveUpDown2D : MonoBehaviour
{
    public float amplitude = 2f; // độ cao
    public float speed = 2f;     // tốc độ

    private Rigidbody2D rb;
    private Vector2 startPos;
    private float time;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = rb.position;
    }

    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;

        float yOffset = Mathf.Sin(time * speed) * amplitude;

        Vector2 newPos = new Vector2(
            startPos.x,
            startPos.y + yOffset
        );

        rb.MovePosition(newPos);
    }
}
