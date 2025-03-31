using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Items
{
    public class ItemFactory : MonoBehaviour
    {
        [SerializeField] private ItemData target;
        [SerializeField] private float productionTime = 5f;
        private int _itemAmount = 0;
        private ItemRegistry _itemRegistry;

        public event Action<string, int> OnUpdate;

        [Inject]
        public void Construct(ItemRegistry itemRegistry)
        {
            _itemRegistry = itemRegistry;
        }

        private void Start()
        {
            OnUpdate?.Invoke(target.name, _itemAmount);
            StartCoroutine(ProduceResourceCoroutine());
        }

        private IEnumerator ProduceResourceCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(productionTime);

                _itemAmount++;

                _itemRegistry.AddItems(target, _itemAmount);

                OnUpdate?.Invoke(target.name, _itemAmount);
            }
        }

        public int CollectResources()
        {
            var collected = _itemAmount;
            _itemAmount = 0;
            return collected;
        }
    }
}