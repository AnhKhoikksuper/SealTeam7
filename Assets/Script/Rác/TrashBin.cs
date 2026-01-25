using UnityEngine;

public class TrashBin : MonoBehaviour
{
    private bool playerNear = false;

    void Update()
    {
        if (!playerNear) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Inventory.Instance == null)
            {
                Debug.LogError("❌ Inventory.Instance chưa được gán!");
                return;
            }

            bool success = Inventory.Instance.RemoveTrash(1);

            if (success)
            {
                if (LevelManager.Instance != null)
                {
                    LevelManager.Instance.AddProgress(1);
                }

                Debug.Log("✅ Đã nộp 1 rác");
            }
            else
            {
                Debug.Log("⚠️ Không còn rác để nộp");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            Debug.Log("👀 Player đứng gần thùng rác (nhấn E)");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            Debug.Log("🚶 Player rời khỏi thùng rác");
        }
    }
}
