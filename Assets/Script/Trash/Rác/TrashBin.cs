using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TrashBin : MonoBehaviour
{
    public AudioClip trashBinSound;
    private AudioSource audioSource;

    // ⭐ chống cộng điểm liên tục khi collider chồng
    private bool cooldown = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (cooldown) return;

        if (!other.CompareTag("Player")) return;

        if (!other.TryGetComponent(out PlayerTrashHolder holder))
        {
            Debug.LogWarning("❌ Player thiếu PlayerTrashHolder");
            return;
        }

        if (!holder.isHoldingTrash)
        {
            Debug.Log("⚠️ Player không có rác");
            return;
        }

        // ⭐ Bỏ rác trước
        holder.DropTrash();

        // ⭐ Cộng điểm chắc chắn
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.AddScore(1);
            Debug.Log($"⭐ Score hiện tại: {LevelManager.Instance.currentScore}");
        }
        else
        {
            Debug.LogError("❌ Không tìm thấy LevelManager!");
        }

        // ⭐ Âm thanh
        if (trashBinSound != null)
            audioSource.PlayOneShot(trashBinSound);

        // ⭐ cooldown nhỏ tránh spam
        cooldown = true;
        Invoke(nameof(ResetCooldown), 0.2f);

        Debug.Log("✅ Đã nộp rác +1 điểm");
    }

    void ResetCooldown()
    {
        cooldown = false;
    }
}
