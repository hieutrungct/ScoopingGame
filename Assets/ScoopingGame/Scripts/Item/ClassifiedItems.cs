using TMPro;
using UnityEngine;

public class ClassifiedItems : MonoBehaviour
{
    public Rarity rarity;
    public RectTransform position;
    public TextMeshProUGUI numberText;
    public void SetUp(int number)
    {
        numberText.text = number.ToString();
    }
}
