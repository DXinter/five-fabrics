using Menu;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class CloseButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private BaseMenu menu;

        private void Awake()
        {
            button.onClick.AddListener(menu.CloseMenu);
        }
        
    }
}