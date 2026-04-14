using TMPro;
using UnityEngine;
namespace Rubik.ScoopingGame
{
    public class ClassifiedItems : MonoBehaviour
    {
        public TinyType tinyType;
        public RectTransform position;
        public TextMeshProUGUI numberText;
        public int number;
        public void SetUp()
        {
            this.number += 1;
            numberText.text = number.ToString();
        }
    }
}
