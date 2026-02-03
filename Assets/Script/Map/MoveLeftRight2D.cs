using UnityEngine;

public class MoveLeftRight2D : MonoBehaviour
{
    public float leftX = -3f;   // Vị trí bên trái
    public float rightX = 3f;   // Vị trí bên phải
    public float speed = 2f;

    private bool movingRight = true;

    void Update()
    {
        float newX = transform.position.x +
                     (movingRight ? speed : -speed) * Time.deltaTime;

        if (newX >= rightX)
            movingRight = false;
        else if (newX <= leftX)
            movingRight = true;

        transform.position = new Vector3(
            newX,
            transform.position.y,
            transform.position.z
        );
    }
}
