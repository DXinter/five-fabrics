using UnityEngine;

namespace Menu
{
    public class BaseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject menuPanel;
        public static bool IsMenuOpen { get; private set; } = false;

        protected virtual void Awake()
        {
            if (menuPanel != null)
                menuPanel.SetActive(false);
        }

        public virtual void OpenMenu()
        {
            if (menuPanel != null)
            {
                menuPanel.SetActive(true);
                IsMenuOpen = true;
            }
        }

        public virtual void CloseMenu()
        {
            if (menuPanel != null)
            {
                menuPanel.SetActive(false);
                IsMenuOpen = false;
            }
        }

        public virtual void ToggleMenu()
        {
            if (menuPanel != null)
            {
                if (menuPanel.activeSelf)
                    CloseMenu();
                else
                    OpenMenu();
            }
        }
    }
}