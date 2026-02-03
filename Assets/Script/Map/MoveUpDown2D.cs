using UnityEngine;

public class MoveUpDown2D : MonoBehaviour
{
    public float amplitude = 2f;   // Độ cao di chuyển
    public float speed = 2f;       // Tốc độ

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(
            startPos.x,
            startPos.y + yOffset,
            startPos.z
        );
    }
}
