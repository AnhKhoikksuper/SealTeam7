using UnityEngine;

public class ItemSpawnerOnRoad : MonoBehaviour
{
    public GameObject itemPrefab;
    public Transform player;

    [Header("Spawn Distance")]
    public float minDistance = 5f;
    public float maxDistance = 12f;

    [Header("Road Y Position")]
    public float groundY = -2.5f;

    [Header("Spawn Control")]
    public float spawnCooldown = 1.5f;

    private float lastSpawnX;

    void Start()
    {
        lastSpawnX = player.position.x;
    }

    void Update()
    {
        if (player.position.x > lastSpawnX + spawnCooldown)
        {
            SpawnItem();
            lastSpawnX = player.position.x;
        }
    }

    void SpawnItem()
    {
        float randomOffset = Random.Range(minDistance, maxDistance);
        float spawnX = player.position.x + randomOffset;

        Vector2 spawnPos = new Vector2(spawnX, groundY);
        Instantiate(itemPrefab, spawnPos, Quaternion.identity);
    }
}
