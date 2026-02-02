using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public float offsetX = 0f;

    void LateUpdate()
    {
        if (!target) return;

        Vector3 pos = transform.position;
        pos.x = Mathf.Lerp(pos.x, target.position.x + offsetX, smoothSpeed * Time.deltaTime);
        transform.position = pos;
    }
}
