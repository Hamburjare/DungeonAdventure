using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class InteractUI : UIManager
    {
        public GameObject interactInfoText;

        private static GameObject s_InteractInfoText;

        public static void ActivateInfoText(bool value)
        {
            s_InteractInfoText.SetActive(value);
        }

        private void Start()
        {
            s_InteractInfoText = interactInfoText;
            CloseAllUI();
        }
    }
}