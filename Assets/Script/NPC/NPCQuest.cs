using UnityEngine;

public class NPCQuest : MonoBehaviour
{
    [TextArea]
    public string questText = "Hãy nhặt 5 rác và bỏ vào thùng để bảo vệ môi trường!";

    public int targetTrash = 5;
    public int rewardGold = 20;

    private bool playerNear = false;
    private bool questGiven = false;
    private bool questFinished = false;

    private void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        if (UIManager.Instance == null || LevelManager.Instance == null)
            return;

        // CHƯA nhận quest
        if (!questGiven)
        {
            StartQuest();
            return;
        }

        // ĐÃ hoàn thành quest
        if (questGiven && LevelManager.Instance.IsQuestCompleted() && !questFinished)
        {
            FinishQuest();
            return;
        }

        // Đã xong hết
        if (questFinished)
        {
            UIManager.Instance.ShowQuest("🌱 Cảm ơn bạn! Hãy tiếp tục bảo vệ môi trường nhé!");
        }
    }

    void StartQuest()
    {
        questGiven = true;

        UIManager.Instance.ShowQuest(questText);
        LevelManager.Instance.StartQuest(targetTrash, rewardGold);

        Debug.Log("📜 Nhận nhiệm vụ");
    }

    void FinishQuest()
    {
        questFinished = true;

        UIManager.Instance.ShowQuest("✅ Hoàn thành nhiệm vụ!\nNhấn E để nhận nhiệm vụ tiếp theo");

        Debug.Log("🎉 NPC xác nhận hoàn thành nhiệm vụ");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNear = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNear = false;
    }
}
