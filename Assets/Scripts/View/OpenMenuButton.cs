using Menu;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class OpenMenuButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private BaseMenu menu;

        private void Awake()
        {
            button.onClick.AddListener(menu.OpenMenu);
        }

    }
}