using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI itemText;
    [SerializeField] private Button slotButton;

    public void SetText(string text)
    {
        itemText.text = text;
    }

    public void SetImage(Sprite newImage)
    {
        image.sprite = newImage;
    }
}
