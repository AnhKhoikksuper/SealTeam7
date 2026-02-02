using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Level Settings")]
    public int trashToSpawn = 10; // ⭐ chỉnh số rác ở đây

    public int currentScore = 0;
    public int targetScore = 10;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        targetScore = trashToSpawn;
    }

    public void AddScore(int amount)
{
    currentScore += amount;

    if (UIManager.Instance != null)
        UIManager.Instance.UpdateScore(currentScore, targetScore);
}

    public void ResetScore()
    {
        currentScore = 0;
    }
}
