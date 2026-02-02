using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public bool questActive = false;
    private bool questCompleted = false;

    public int currentProgress = 0;
    public int targetProgress = 5;
    public int rewardGold = 20;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // NPC gọi khi nhận quest
    public void StartQuest(int target, int reward)
    {
        questActive = true;
        questCompleted = false;

        currentProgress = 0;
        targetProgress = target;
        rewardGold = reward;

        Debug.Log("📜 Quest bắt đầu");

        if (UIManager.Instance != null)
            UIManager.Instance.UpdateScore(currentProgress, targetProgress);
    }

    // TrashBin gọi khi bỏ rác
    public void AddProgress(int amount)
    {
        if (!questActive)
        {
            Debug.Log("⚠️ Quest chưa kích hoạt");
            return;
        }

        if (questCompleted) return;

        currentProgress += amount;

        // Không cho vượt quá target
        currentProgress = Mathf.Clamp(currentProgress, 0, targetProgress);

        Debug.Log($"🧮 Progress: {currentProgress}/{targetProgress}");

        if (UIManager.Instance != null)
            UIManager.Instance.UpdateScore(currentProgress, targetProgress);

        if (currentProgress >= targetProgress)
        {
            CompleteQuest();
        }
    }

    void CompleteQuest()
    {
        if (questCompleted) return;

        questCompleted = true;
        questActive = false;

        if (Inventory.Instance != null)
            Inventory.Instance.AddGold(rewardGold);

        if (UIManager.Instance != null)
            UIManager.Instance.ShowQuestComplete();

        Debug.Log("✅ Hoàn thành nhiệm vụ");
    }

    public bool IsQuestCompleted()
    {
        return questCompleted;
    }
}
