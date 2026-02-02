using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public int trashInBag = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddTrash(int amount)
    {
        trashInBag += amount;
        Debug.Log("Rác trong túi: " + trashInBag);
    }

    public bool HasTrash()
    {
        return trashInBag > 0;
    }

    public void RemoveTrash(int amount)
    {
        trashInBag -= amount;
        if (trashInBag < 0) trashInBag = 0;
    }
}
