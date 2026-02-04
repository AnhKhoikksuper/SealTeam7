using UnityEngine;
using TMPro;
using System.Collections;

public class NPCQuest : MonoBehaviour
{
    [Header("Refs")]
    public GameObject canvasPlayer;
    public GameManager gameManger;
    [Header("Player")]
    public PlayerMovement playerMovement;
    [Header("Camera")]
    public Camera cam;
    public GameObject CineMachine;
    [Header("Zoom Camera")]
    public Camera zoomCam;
    [Header("DialogueUI")]
    public GameObject dialoguePanel; // ⭐ GameObject cha chứa text
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI startText;


    [Header("Zoom")]
    public float zoomSize = 3f;
    public float zoomSpeed = 1.5f;


    int binCount;
    int trashCount;

    bool playerInside;
    bool dialogueDone;
    bool questStarted;

    Vector3 camOriginalPos;
    float camOriginalSize;

    ItemSpawner spawner;

    void Start()
    {
        canvasPlayer.SetActive(false);
        spawner = FindFirstObjectByType<ItemSpawner>();

        camOriginalPos = cam.transform.position;
        camOriginalSize = cam.orthographicSize;

        if (dialoguePanel)
            dialoguePanel.SetActive(false);

        dialogueText.text = "";

        if (startText)
            startText.gameObject.SetActive(false);

        if (zoomCam)
            zoomCam.gameObject.SetActive(false); // ⭐ tắt cam phụ
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || questStarted) return;
        Debug.Log("Cham Player");
        playerInside = true;
        StartCoroutine(QuestSequence(other.transform));

    }

    IEnumerator QuestSequence(Transform player)
    {
        canvasPlayer.SetActive(false);
        gameManger.isRunTimer = false;
        playerMovement.canControl = false;

        dialoguePanel.SetActive(true);

        // ⭐ Random trước để NPC biết mà nói
        binCount = Random.Range(1, 4);
        trashCount = binCount + Random.Range(3, 6);
        trashCount = Mathf.Clamp(trashCount, 3, 15);

        // ⭐ chuyển sang camera phụ
        cam.gameObject.SetActive(false);
        CineMachine.SetActive(false);
        zoomCam.gameObject.SetActive(true);

        // ⭐ zoom camera
        yield return Zoom();

        // ⭐ Hội thoại gộp 1 lần
        string fullDialogue =
            "Này! Anh Hùng Nhặt Rác!\n" +
            $"- Có khoảng {trashCount} rác đang nằm rải rác.\n" +
            $"- Có khoảng {binCount} thùng rác hỗ trợ bạn.\n" +
            "- Nhặt rác và bỏ đúng thùng!\n" +
            "- Hoàn thành trước khi hết giờ!\n";

        // 👉 Nếu muốn hiện ngay lập tức:
        dialogueText.text = fullDialogue;

        // 👉 Nếu muốn hiệu ứng gõ chữ thì dùng dòng này thay cho dòng trên:
        // yield return TypeLine(fullDialogue);

        dialogueDone = true;
        startText.gameObject.SetActive(true);
        startText.text = "Nhấn [Space] để bắt đầu";
    }




    void Update()
    {
        if (!playerInside || !dialogueDone || questStarted) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Invoke(nameof(BeginGame), 0.2f); // delay 1 giây
        }
    }


    void BeginGame()
    {
        questStarted = true;

        LevelManager.Instance.SetupLevel(trashCount);

        spawner.binCount = binCount;
        spawner.SpawnAll();

        canvasPlayer.SetActive(true);
        gameManger.isRunTimer = true;

        // ⭐ chuyển lại camera chính
        zoomCam.gameObject.SetActive(false);
        cam.gameObject.SetActive(true);
        CineMachine.SetActive(true);

        playerMovement.canControl = true;

        cam.transform.position = camOriginalPos;
        cam.orthographicSize = camOriginalSize;

        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        startText.gameObject.SetActive(false);
    }
    IEnumerator Zoom()
    {
        float t = 0;
        float startSize = zoomCam.orthographicSize;

        while (t < 1)
        {
            t += Time.deltaTime * zoomSpeed;

            zoomCam.orthographicSize =
                Mathf.Lerp(startSize, zoomSize, t);

            yield return null;
        }
    }
}
