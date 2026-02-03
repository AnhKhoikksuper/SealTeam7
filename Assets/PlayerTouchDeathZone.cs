using UnityEngine;

public class PlayerTouchDeathZone : MonoBehaviour
{
    public LayerMask deathLayer;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra layer DeathZone
        if (((1 << other.gameObject.layer) & deathLayer) != 0)
        {
            Debug.Log("Play cham DeathZone");
            if (GameManager.Instance != null)
            {
                GameManager.Instance.Lose();
                Debug.Log("Chay ham thua ne");
            }
        }
    }
    
}
