using UnityEngine;

public class TrashDestroy : MonoBehaviour
{
    public float destroyY = -6f;

    void Update()
    {
        if (transform.position.y < destroyY)
        {
            Destroy(gameObject);
        }
    }
}
