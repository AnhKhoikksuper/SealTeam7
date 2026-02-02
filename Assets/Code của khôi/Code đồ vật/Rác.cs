using UnityEngine;

public class Trash : MonoBehaviour
{
    public int amount = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory.Instance.AddTrash(amount);
            Destroy(gameObject);
        }
    }
}
