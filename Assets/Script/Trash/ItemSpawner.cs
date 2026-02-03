using UnityEngine;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject trashPrefab;
    public GameObject binPrefab;
    [Header("Edge Check")]
    public float edgeCheckDistance = 0.6f;

    [Header("Counts")]
    public int binCount = 2;

    [Header("Spawn Range X")]
    public float minX = 0f;
    public float maxX = 20f;
    [Header("Spawn Height Range Y")]
    public float minRayY = 5f;
    public float maxRayY = 50f;

    [Header("Ground")]
    public LayerMask groundLayer;
    public float yOffset = 0.3f;

    [Header("Distance Rules")]
    public float minDistanceFromCharacter = 3f;
    public float minDistanceBetweenSpawns = 2f;

    public Transform player;
    public Transform NPC;
    List<Vector2> usedPositions = new List<Vector2>();

    // ⭐ NPC gọi hàm này
    public void SpawnAll()
    {
        usedPositions.Clear();

        SpawnTrash();
        SpawnBins();
    }

    void SpawnTrash()
    {
        int trashCount = LevelManager.Instance.trashToSpawn;

        for (int i = 0; i < trashCount; i++)
        {
            Vector2 pos = GetValidPos();

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
            Vector2 pos = GetValidPos();

            if (pos != Vector2.zero)
            {
                Instantiate(binPrefab, pos, Quaternion.identity);
                usedPositions.Add(pos);
            }
        }

        Debug.Log($"🧺 Spawned {binCount} bins");
    }

    Vector2 GetValidPos()
    {
        for (int i = 0; i < 50; i++)
        {
            float x = Random.Range(minX, maxX);
            float rayY = Random.Range(minRayY, maxRayY);
            RaycastHit2D hit = Physics2D.Raycast(
                new Vector2(x, rayY),
                Vector2.down,
                100,
                groundLayer
            );

        if (!hit) continue;

        Vector2 pos = hit.point + Vector2.up * yOffset;

        // ===== ❌ Check rìa ground =====
        bool leftGround = Physics2D.Raycast(
            pos + Vector2.left * edgeCheckDistance,
            Vector2.down,
            1f,
            groundLayer
        );

        bool rightGround = Physics2D.Raycast(
            pos + Vector2.right * edgeCheckDistance,
            Vector2.down,
            1f,
            groundLayer
        );

        if (!leftGround || !rightGround)
            continue;
        // ============================

        // ❌ gần Player
        if (player &&
            Vector2.Distance(pos, player.position) < minDistanceFromCharacter)
            continue;

        // ❌ gần NPC
        if (NPC &&
            Vector2.Distance(pos, NPC.position) < minDistanceFromCharacter)
            continue;

        // ❌ gần spawn khác
        bool tooClose = false;
        foreach (var p in usedPositions)
        {
            if (Vector2.Distance(pos, p) < minDistanceBetweenSpawns)
            {
                tooClose = true;
                break;
            }
        }

        if (tooClose) continue;

        return pos;
    }

        return Vector2.zero;
    }
}