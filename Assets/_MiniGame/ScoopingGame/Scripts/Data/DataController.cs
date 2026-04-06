using UnityEngine;
namespace Rubik
{
    public class DataController : MonoBehaviour
    {
        public static DataController instance;
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }
}