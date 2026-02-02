using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public AudioClip trashBinSound;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerTrashHolder holder = other.GetComponent<PlayerTrashHolder>();

        if (holder == null) return;

        if (!holder.isHoldingTrash)
        {
            Debug.Log("⚠️ Player không có rác");
            return;
        }

        // Bỏ rác
        holder.DropTrash();

        // Cộng điểm
        if (LevelManager.Instance != null)
            LevelManager.Instance.AddProgress(1);
        if (audioSource != null && trashBinSound != null)
        {
            audioSource.PlayOneShot(trashBinSound);
        }
        Debug.Log("✅ Đã nộp rác +1 điểm");
    }
}
