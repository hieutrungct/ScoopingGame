using UnityEngine;
using UnityEngine.UI;

public class RewardItem : MonoBehaviour
{
    public string id;
    public Image icon;
    public Rarity rarity;
    public void SetUp(string id, Sprite sprite, Rarity rarity)
    {
        this.id = id;
        this.rarity = rarity;
        icon.sprite = sprite;
    }
}
