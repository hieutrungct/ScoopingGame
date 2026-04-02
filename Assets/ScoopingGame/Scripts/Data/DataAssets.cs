using UnityEngine;
namespace Rubik
{
    public class DataAssets : MonoBehaviour
    {
        public static DataAssets instance;
        public LoadImage loadImage;
        private void Awake()
        {
            instance = this;
            loadImage.LoadAllSprites();
        }

    }
}
