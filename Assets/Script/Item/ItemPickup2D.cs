using UnityEngine;

public class ItemPickup2D : MonoBehaviour
{
    [Header("Item Info")]
    public string itemName = "Item";

    void Start()
    {
        
    }

    void Update()
    {
            Pickup();
    }

    void Pickup()
    {
        Debug.Log("Picked up: " + itemName);
                
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Rac cham Player");
            Destroy(this.gameObject);
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
        }
    }
}
