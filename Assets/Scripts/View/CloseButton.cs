using Menu;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class CloseButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private BackpackMenu backpackMenu;

        private void Awake()
        {
            button.onClick.AddListener(backpackMenu.CloseMenu);
        }
        
    }
}