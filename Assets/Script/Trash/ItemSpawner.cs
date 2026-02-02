using UnityEngine;
using System.Collections.Generic;

public class TrashSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject trashPrefab;
    public GameObject binPrefab;

    [Header("Player")]
    public Transform player;
    public float safeDistanceFromPlayer = 3f;

    [Header("Trash Spawn Settings")]
    public int trashCount = 10;
    public float trashMinX = 0f;
    public float trashMaxX = 20f;
    public float trashY = -2.5f;

    [Header("Bin Spawn Settings")]
    public int binCount = 2;
    public float binMinX = 0f;
    public float binMaxX = 20f;
    public float binY = -2.5f;

    [Header("Spawn Safety")]
    public float minDistanceBetweenObjects = 2f;
    public int maxAttempts = 30;

    private List<Vector2> usedPositions = new List<Vector2>();

    void Start()
    {
        SpawnTrash();
        SpawnBins();
        SyncQuestWithSpawn();
    }

    // 🗑️ Spawn rác
    void SpawnTrash()
    {
        for (int i = 0; i < trashCount; i++)
        {
            Vector2 pos = GetValidPosition(trashMinX, trashMaxX, trashY);

            Instantiate(trashPrefab, pos, Quaternion.identity);
            usedPositions.Add(pos);

            Debug.Log("🗑️ Trash at: " + pos);
        }
    }

    // 🧺 Spawn thùng rác
    void SpawnBins()
    {
        for (int i = 0; i < binCount; i++)
        {
            Vector2 pos = GetValidPosition(binMinX, binMaxX, binY);

            Instantiate(binPrefab, pos, Quaternion.identity);
            usedPositions.Add(pos);

            Debug.Log("🧺 Bin at: " + pos);
        }
    }

    // 📍 Tìm vị trí hợp lệ
    Vector2 GetValidPosition(float minX, float maxX, float y)
    {
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            float x = Random.Range(minX, maxX);
            Vector2 candidate = new Vector2(x, y);

            // ❌ Gần player
            if (player != null &&
                Vector2.Distance(candidate, player.position) < safeDistanceFromPlayer)
                continue;

            // ❌ Gần object khác
            bool tooClose = false;
            foreach (var pos in usedPositions)
            {
                if (Vector2.Distance(candidate, pos) < minDistanceBetweenObjects)
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose)
                return candidate;
        }

        // fallback nếu quá nhiều lần fail
        return new Vector2(Random.Range(minX, maxX), y);
    }

    // 📊 Sync quest
    void SyncQuestWithSpawn()
    {
        if (LevelManager.Instance == null) return;

        LevelManager.Instance.StartQuest(trashCount, LevelManager.Instance.rewardGold);

        Debug.Log($"📊 Quest synced: 0/{trashCount}");
    }
}
