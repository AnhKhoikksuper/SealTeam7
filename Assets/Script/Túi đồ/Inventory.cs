using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public int trashInBag = 0;
    public int gold = 0;

    public TrashSlotUI trashSlotUI;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Nhặt rác → CHỈ vào túi
    public void AddTrash(int amount)
    {
        trashInBag += amount;

        if (trashSlotUI != null)
            trashSlotUI.UpdateCount(trashInBag);
    }

    // Bỏ rác → TRẢ TRUE nếu thành công
    public bool RemoveTrash(int amount)
    {
        if (trashInBag >= amount)
        {
            trashInBag -= amount;

            if (trashSlotUI != null)
                trashSlotUI.UpdateCount(trashInBag);

            return true;
        }
        return false;
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UIManager.Instance.UpdateGold(gold);
    }
    private void Start()
{
    if (trashSlotUI != null)
        trashSlotUI.UpdateCount(trashInBag);
}

}
