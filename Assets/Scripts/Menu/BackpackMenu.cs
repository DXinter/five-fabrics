using UnityEngine;

namespace Menu
{
    public class BackpackMenu : MonoBehaviour
    {
        public GameObject menuPanel;
        public static bool IsMenuOpen = false;

        private void Awake()
        {
            menuPanel.SetActive(false);
        }

        public void OpenMenu()
        {
            menuPanel.SetActive(true);
            IsMenuOpen = true;
        }

        public void CloseMenu()
        {
            menuPanel.SetActive(false);
            IsMenuOpen = false;
        }
    }
}