using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI")]
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateScore(0, LevelManager.Instance.targetScore);
    }

    // ⭐ Hàm gọi khi điểm thay đổi
    public void UpdateScore(int current, int target)
    {
        if (scoreText == null) return;

        scoreText.text = $"Score: {current}/{target}";
    }
}
