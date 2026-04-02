using UnityEngine;
using UnityEngine.UI;

public class ItemFly : MonoBehaviour
{
    [SerializeField] private Image image;
    public void SetUpImage(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
