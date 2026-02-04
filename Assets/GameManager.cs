using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Time")]
    public float timeLimit = 120f;
    private float timeLeft;
    private bool gameEnded = false;
    public bool isRunTimer;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject pausePanel;

    private bool canPressKey = false;
    private bool isPaused = false;

    [Header("Audio")]
    public AudioClip bgm;
    private AudioSource audioSource;

    // ⭐ Danh sách map
    string[] maps = { "Map1", "Map2", "Map3", "Map4", "Map5" };

    void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        Time.timeScale = 1;
        timeLeft = timeLimit;
        isRunTimer = false;

        if (winPanel) winPanel.SetActive(false);
        if (losePanel) losePanel.SetActive(false);
        if (pausePanel) pausePanel.SetActive(false);

        PlayBGM();
    }

    void Update()
    {
        if (!gameEnded && Input.GetKeyDown(KeyCode.Escape))
            TogglePause();

        if (!gameEnded && !isPaused && isRunTimer)
        {
            RunTimer();
            CheckWin();
        }

        HandleInput();
    }

    void PlayBGM()
    {
        if (bgm == null) return;
        audioSource.clip = bgm;
        audioSource.loop = true;
        audioSource.Play();
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
        if (gameEnded) return;

        gameEnded = true;

        if (winPanel) winPanel.SetActive(true);

        StartCoroutine(WinDelay());
    }

    IEnumerator WinDelay()
    {
        canPressKey = false;
        yield return new WaitForSeconds(1f);
        canPressKey = true;
    }

    public void Lose()
    {
        if (gameEnded) return;

        gameEnded = true;

        if (losePanel) losePanel.SetActive(true);

        Time.timeScale = 0;
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (pausePanel) pausePanel.SetActive(isPaused);

        Time.timeScale = isPaused ? 0 : 1;
    }

    void HandleInput()
    {
        if (isPaused && pausePanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return)) TogglePause();
            if (Input.GetKeyDown(KeyCode.R)) Replay();
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("Menu");
            }
            return;
        }

        if (!gameEnded) return;

        if (winPanel.activeSelf && canPressKey)
        {
            if (Input.GetKeyDown(KeyCode.Return)) LoadNextLevel();
            if (Input.GetKeyDown(KeyCode.Backspace))
                SceneManager.LoadScene("Menu");
            if (Input.GetKeyDown(KeyCode.R)) Replay();
        }

        if (losePanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.R)) Replay();
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("Menu");
            }
        }
    }

    public void Replay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ⭐ RANDOM MAP KHÔNG TRÙNG MAP HIỆN TẠI
    public void LoadNextLevel()
    {
        Time.timeScale = 1;

        string currentMap =
            SceneManager.GetActiveScene().name;

        List<string> availableMaps =
            new List<string>(maps);

        // ❌ bỏ map hiện tại
        availableMaps.Remove(currentMap);

        // 🎲 random map còn lại
        string nextMap =
            availableMaps[
                Random.Range(0, availableMaps.Count)
            ];

        SceneManager.LoadScene(nextMap);
    }
}
