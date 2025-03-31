using Settings;
using UnityEngine;
using Zenject;

namespace View
{
    public class ItemsListView : MonoBehaviour
    {
        [SerializeField] private RectTransform itemsContainer;
        private ItemsSettings _itemsSettings;
        private PlayerCollectedItemsView.Factory _offerViewFactory;

        [Inject]
        public void Construct(PlayerCollectedItemsView.Factory offerViewFactory, ItemsSettings settings)
        {
            _itemsSettings = settings;
            _offerViewFactory = offerViewFactory;
        }

        private void Awake()
        {
            foreach (var data in _itemsSettings.itemData)
            {
                var view = _offerViewFactory.Create(data);
                view.transform.SetParent(itemsContainer, false);
            }
        }
    }
}