using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject trashPrefab;
    public GameObject binPrefab;

    [Header("Bin Settings")]
    public int binCount = 2;

    [Header("Spawn Range X")]
    public float minX = 0f;
    public float maxX = 20f;

    [Header("Ground")]
    public LayerMask groundLayer;
    public float yOffset = 0.3f;

    public Transform player;
    public float minDistanceFromPlayer = 3f;

    void Start()
    {
        SpawnTrashFromLevel();
        SpawnBins();

        if (LevelManager.Instance != null)
            LevelManager.Instance.ResetScore();
    }

    // ⭐ Lấy số rác từ LevelManager
    void SpawnTrashFromLevel()
    {
        if (LevelManager.Instance == null) return;

        int trashCount = LevelManager.Instance.trashToSpawn;

        for (int i = 0; i < trashCount; i++)
        {
            Vector2 pos = GetGroundPos();
            Instantiate(trashPrefab, pos, Quaternion.identity);
        }

        Debug.Log($"🗑️ Spawned {trashCount} trash");
    }

    void SpawnBins()
    {
        for (int i = 0; i < binCount; i++)
        {
            Vector2 pos = GetGroundPos();
            Instantiate(binPrefab, pos, Quaternion.identity);
        }

        Debug.Log($"🧺 Spawned {binCount} bins");
    }

    Vector2 GetGroundPos()
    {
        for (int i = 0; i < 30; i++)
        {
            float x = Random.Range(minX, maxX);

            RaycastHit2D hit = Physics2D.Raycast(
                new Vector2(x, 50),
                Vector2.down,
                100,
                groundLayer
            );

            if (!hit) continue;

            Vector2 pos = hit.point + Vector2.up * yOffset;

            if (Vector2.Distance(pos, player.position) < minDistanceFromPlayer)
                continue;

            return pos;
        }

        return Vector2.zero;
    }
}
