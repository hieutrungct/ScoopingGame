using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NTPackage.UI;

namespace Rubik.UI
{
    public class MessagePanel : PopupUI
    {
        public TextMeshProUGUI txtTitle;
        public TextMeshProUGUI txtContent;

        
        public void SetData(string title, string content)
        {
            txtTitle.text = title;
            txtContent.text = content;
        }
    }
}