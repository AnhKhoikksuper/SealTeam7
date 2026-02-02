using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Header("Quest")]
    public int requiredTrash = 3;
    public int currentTrash = 0;
    public bool questCompleted = false;

    [Header("Score")]
    public int score = 0;
    public int scorePerTrash = 10;

    [Header("UI")]
    public TextMeshProUGUI questText;
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        Instance = this;
        UpdateUI();
    }

    public void AddTrashToQuest(int amount)
    {
        if (questCompleted) return;

        currentTrash += amount;
        score += amount * scorePerTrash;

        if (currentTrash >= requiredTrash)
        {
            questCompleted = true;
            questText.text = "🎉 Nhiệm vụ hoàn thành!";
        }
        else
        {
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        questText.text = "Nhặt rác: " + currentTrash + " / " + requiredTrash;
        scoreText.text = "Điểm: " + score;
    }
}
