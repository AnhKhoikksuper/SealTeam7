using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI trashText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI questText;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateTrash(int value)
    {
        trashText.text = "Rác: " + value;
    }

    public void UpdateScore(int current, int target)
    {
        scoreText.text = "Điểm: " + current + "/" + target;
    }

    public void UpdateGold(int value)
    {
        goldText.text = "Vàng: " + value;
    }

    public void ShowQuest(string text)
    {
        questText.text = text;
    }

    internal void ShowQuestComplete()
    {
        questText.text = "✅ Hoàn thành nhiệm vụ!";
    }
}
