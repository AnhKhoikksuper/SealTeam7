using UnityEngine;

public class Trash : MonoBehaviour
{
    [Header("Item Info")]
    public string itemName = "Item";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerTrashHolder holder = other.GetComponent<PlayerTrashHolder>();

            // Nếu đang cầm rác thì không nhặt
            if (holder != null && holder.isHoldingTrash)
            {
                Debug.Log("⚠️ Đang cầm rác rồi!");
                return;
            }

            if (holder != null)
            {
                holder.PickTrash();
            }

            Debug.Log("✅ Nhặt rác: " + itemName);
            Destroy(gameObject);
        }
    }
}
