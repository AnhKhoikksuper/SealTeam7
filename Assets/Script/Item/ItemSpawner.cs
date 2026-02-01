using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;

    [Header("Spawn Settings")]
    public int spawnCount = 10;     // Spawn bao nhiêu rác
    public float minX = 0f;         // Spawn từ X nào
    public float maxX = 20f;        // Đến X nào
    public float spawnY = -2.5f;    // Chiều cao spawn

    void Start()
    {
        SpawnItems();
    }

    void SpawnItems()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            float randomX = Random.Range(minX, maxX);
            Vector2 spawnPos = new Vector2(randomX, spawnY);

            Instantiate(itemPrefab, spawnPos, Quaternion.identity);

            Debug.Log("Spawned at: " + spawnPos);
        }
    }
}
