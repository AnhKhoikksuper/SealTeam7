using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuEnterGame : MonoBehaviour
{
    [Header("Text")]
    public TextMeshProUGUI text;

    [Header("Pulse Settings")]
    public float pulseSpeed = 2f;
    public float pulseAmount = 0.2f;

    private Vector3 baseScale;

    // ⭐ Danh sách map
    string[] maps = { "Map1", "Map2", "Map3", "Map4", "Map5" };

    void Start()
    {
        if (text != null)
            baseScale = text.transform.localScale;
    }

    void Update()
    {
        // ===== Hiệu ứng phóng to thu nhỏ =====
        if (text != null)
        {
            float scaleOffset = Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
            text.transform.localScale = baseScale * (1f + scaleOffset);
        }

        // ===== Nhấn Enter để sang scene random =====
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoadRandomScene();
        }
    }

    void LoadRandomScene()
    {
        int rand = Random.Range(0, maps.Length);
        SceneManager.LoadScene(maps[rand]);
    }
}
