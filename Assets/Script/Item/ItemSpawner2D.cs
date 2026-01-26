using UnityEngine;

public class ItemSpawner2D : MonoBehaviour
{
    public GameObject itemPrefab;
    public float spawnInterval = 1.5f;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        InvokeRepeating(nameof(SpawnItem), 0f, spawnInterval);
    }

    void SpawnItem()
    {
        // Lấy biên camera
        Vector3 leftTop = cam.ViewportToWorldPoint(new Vector3(0, 1, 0));
        Vector3 rightTop = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

        // Random X trong camera
        float randomX = Random.Range(leftTop.x, rightTop.x);

        // Spawn sát mép trên camera
        float spawnY = leftTop.y + 0.2f;

        Instantiate(itemPrefab, new Vector2(randomX, spawnY), Quaternion.identity);
    }
}
