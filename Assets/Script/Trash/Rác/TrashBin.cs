using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class TrashBin : MonoBehaviour
{
    public AudioClip trashBinSound;
    private AudioSource audioSource;

    // ⭐ hiệu ứng rác rơi
    public GameObject trashDown;
    public float dropDistance = 1.5f;
    public float dropDuration = 0.3f;

    private bool cooldown = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (trashDown != null)
            trashDown.SetActive(false);
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

        // ⭐ Bỏ rác
        holder.DropTrash();

        // ⭐ Cộng điểm
        if (LevelManager.Instance != null)
            LevelManager.Instance.AddScore(1);

        // ⭐ Âm thanh
        if (trashBinSound != null)
            audioSource.PlayOneShot(trashBinSound);

        // ⭐ chạy hiệu ứng rơi
        if (trashDown != null)
            StartCoroutine(TrashDropEffect());

        cooldown = true;
        Invoke(nameof(ResetCooldown), 0.2f);

        Debug.Log("✅ Đã nộp rác +1 điểm");
    }

    IEnumerator TrashDropEffect()
    {
        if (trashDown == null) yield break;

        // ⭐ luôn reset vị trí trước khi chạy
        Vector3 originalPos = trashDown.transform.localPosition;
        trashDown.transform.localPosition = originalPos;

        // ⭐ chỉ bật lên, không làm gì khác
        trashDown.SetActive(true);

        Debug.Log("Hieu ung chay ne");

        Vector3 targetPos = originalPos + Vector3.down * dropDistance;

        float t = 0f;

        while (t < dropDuration)
        {
            t += Time.deltaTime;

            trashDown.transform.localPosition =
                Vector3.Lerp(originalPos, targetPos, t / dropDuration);

            yield return null;
        }

        // ⭐ đảm bảo rơi tới đáy
        trashDown.transform.localPosition = targetPos;

        // ⭐ rung thùng rác (KHÔNG ảnh hưởng trashDown)
        Vector3 originalScale = transform.localScale;
        transform.localScale = originalScale * 1.2f;

        yield return new WaitForSeconds(0.05f);
        transform.localScale = originalScale;

        // ⭐ RESET sạch
        trashDown.transform.localPosition = originalPos;
        trashDown.SetActive(false);
    }



    void ResetCooldown()
    {
        cooldown = false;
    }
}
