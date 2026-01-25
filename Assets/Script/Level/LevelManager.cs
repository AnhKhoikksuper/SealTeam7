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

        UIManager.Instance.UpdateScore(currentProgress, targetProgress);
    }

    // TrashBin gọi khi bỏ rác
    public void AddProgress(int amount)
    {
        if (!questActive || questCompleted) return;

        currentProgress += amount;
        UIManager.Instance.UpdateScore(currentProgress, targetProgress);

        if (currentProgress >= targetProgress)
        {
            CompleteQuest();
        }
    }

    void CompleteQuest()
    {
        questCompleted = true;
        questActive = false;

        Inventory.Instance.AddGold(rewardGold);
        UIManager.Instance.ShowQuestComplete();

        Debug.Log("✅ Hoàn thành nhiệm vụ");
    }

    // NPC hỏi xem quest xong chưa
    public bool IsQuestCompleted()
    {
        return questCompleted;
    }
}
