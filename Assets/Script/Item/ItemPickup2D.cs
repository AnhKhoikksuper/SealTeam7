using UnityEngine;

public class ItemPickup2D : MonoBehaviour
{
    [Header("Item Info")]
    public string itemName = "Item";

    [Header("UI")]
    public GameObject pickupUI; // Text "F nhặt"

    private bool playerInRange;

    void Start()
    {
        if (pickupUI != null)
            pickupUI.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            Pickup();
        }
    }

    void Pickup()
    {
        Debug.Log("Picked up: " + itemName);

        // InventoryManager.instance.Add(item); // nếu có

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (pickupUI != null)
                pickupUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (pickupUI != null)
                pickupUI.SetActive(false);
        }
    }
}
