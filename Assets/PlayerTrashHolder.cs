using UnityEngine;

public class PlayerTrashHolder : MonoBehaviour
{
    public GameObject holdTrash; // gán trong Inspector
    public AudioClip itemPickupSound;
    private AudioSource audioSource;
    public bool isHoldingTrash = false;

    

    void Start()
    {
        // Lấy AudioSource trên Player
        audioSource = GetComponent<AudioSource>();

        if (holdTrash != null)
            holdTrash.SetActive(false);
    }

    public void PickTrash()
    {
        if (isHoldingTrash) return;

        isHoldingTrash = true;

        if (holdTrash != null)
            holdTrash.SetActive(true);

        // Phát âm thanh
        if (audioSource != null && itemPickupSound != null)
        {
            audioSource.PlayOneShot(itemPickupSound);
        }
            

        Debug.Log("🗑️ Player đang cầm rác");
    }

    public void DropTrash()
    {
        isHoldingTrash = false;

        if (holdTrash != null)
            holdTrash.SetActive(false);

        Debug.Log("♻️ Đã bỏ rác");
    }
}
