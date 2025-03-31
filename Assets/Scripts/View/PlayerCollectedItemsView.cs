using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View
{
    public class PlayerCollectedItemsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text count;
        [SerializeField] private Image avatar;

        private ItemData _itemData;
        private ItemRegistry _itemRegistry;

        [Inject]
        public void Construct(ItemData itemData, ItemRegistry itemRegistry)
        {
            _itemData = itemData;
            _itemRegistry = itemRegistry;
        }

        private void Awake()
        {
            avatar.sprite = _itemData.avatar;
            count.text = 0.ToString();
        }

        private void OnEnable()
        {
            var value = _itemRegistry.GetValue(_itemData);
            count.text = value.ToString();
        }

        public class Factory : PlaceholderFactory<ItemData, PlayerCollectedItemsView>
        {
        }
    }
}