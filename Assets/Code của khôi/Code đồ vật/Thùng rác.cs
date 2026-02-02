using UnityEngine;

public class TrashBin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Inventory.Instance.HasTrash())
        {
            int amount = Inventory.Instance.trashInBag;

            Inventory.Instance.RemoveTrash(amount);
            QuestManager.Instance.AddTrashToQuest(amount);
        }
    }
}
