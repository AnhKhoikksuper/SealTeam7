using UnityEngine;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject trashPrefab;
    public GameObject binPrefab;

    [Header("Counts")]
    public int binCount = 2;

    [Header("Spawn Range X")]
    public float minX = 0f;
    public float maxX = 20f;

    [Header("Ground")]
    public LayerMask groundLayer;
    public float yOffset = 0.3f;

    [Header("Distance Rules")]
    public float minDistanceFromPlayer = 3f;
    public float minDistanceBetweenSpawns = 2f; // ⭐ chống chồng

    public Transform player;

    // ⭐ lưu vị trí đã spawn
    private List<Vector2> usedPositions = new List<Vector2>();

    void Start()
    {
        SpawnTrashFromLevel();
        SpawnBins();

        if (LevelManager.Instance != null)
            LevelManager.Instance.ResetScore();
    }

    // =======================

    void SpawnTrashFromLevel()
    {
        if (LevelManager.Instance == null) return;

        int trashCount = LevelManager.Instance.trashToSpawn;

        for (int i = 0; i < trashCount; i++)
        {
            Vector2 pos = GetValidGroundPos();

            if (pos != Vector2.zero)
            {
                Instantiate(trashPrefab, pos, Quaternion.identity);
                usedPositions.Add(pos);
            }
        }

        Debug.Log($"🗑️ Spawned {trashCount} trash");
    }

    void SpawnBins()
    {
        for (int i = 0; i < binCount; i++)
        {
            Vector2 pos = GetValidGroundPos();

            if (pos != Vector2.zero)
            {
                Instantiate(binPrefab, pos, Quaternion.identity);
                usedPositions.Add(pos);
            }
        }

        Debug.Log($"🧺 Spawned {binCount} bins");
    }

    // =======================

    Vector2 GetValidGroundPos()
    {
        for (int i = 0; i < 50; i++)
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

            // ❌ gần Player
            if (Vector2.Distance(pos, player.position) < minDistanceFromPlayer)
                continue;

            // ❌ gần spawn khác
            bool tooClose = false;
            foreach (var used in usedPositions)
            {
                if (Vector2.Distance(pos, used) < minDistanceBetweenSpawns)
                {
                    tooClose = true;
                    break;
                }
            }

            if (tooClose) continue;

            return pos;
        }

        Debug.LogWarning("⚠️ Không tìm được vị trí spawn hợp lệ");
        return Vector2.zero;
    }
}
