using TMPro;
using UnityEngine;

public class TrashSlotUI : MonoBehaviour
{
    public TextMeshProUGUI countText;

    public void UpdateCount(int value)
    {
        countText.text = value.ToString();
    }
}
