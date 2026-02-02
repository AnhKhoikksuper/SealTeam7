using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Time")]
    public float timeLimit = 120f;
    private float timeLeft;
    private bool gameEnded = false;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public GameObject winPanel;
    public GameObject losePanel;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1;
        timeLeft = timeLimit;
        if (winPanel) winPanel.SetActive(false);
        if (losePanel) losePanel.SetActive(false);
    }

    void Update()
    {
        if (gameEnded) return;

        RunTimer();
        CheckWin();

        if (Input.GetKeyDown(KeyCode.R))
        {
            Replay();
        }
    }

    void RunTimer()
    {
        timeLeft -= Time.deltaTime;
        timeLeft = Mathf.Max(timeLeft, 0);

        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);

        if (timerText != null)
            timerText.text = $"{minutes:00}:{seconds:00}";

        if (timeLeft <= 0)
            Lose();
    }

    void CheckWin()
    {
        if (LevelManager.Instance.currentScore >=
            LevelManager.Instance.targetScore)
        {
            Win();
        }
    }

    public void Win()
    {
        gameEnded = true;
        if (winPanel) winPanel.SetActive(true);
        Debug.Log("🏆 WIN");
    }

    public void Lose()
    {
        gameEnded = true;
        if (losePanel) losePanel.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("💀 LOSE");
    }

    // =========================
    // ⭐ HÀM MỚI
    // =========================

    // Chơi lại
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Sang màn tiếp theo
    public void LoadNextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        // tránh vượt quá số scene trong build
        if (nextScene < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextScene);
        else
            Debug.Log("🚫 Không còn màn tiếp theo");
    }
}
