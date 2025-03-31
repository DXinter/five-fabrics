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
                OnUpdate?.Invoke(target.name, _itemAmount);
            }
        }

        public void CollectResources()
        {
            _itemRegistry.AddItems(target, _itemAmount);
            
            _itemAmount = 0;
            
            OnUpdate?.Invoke(target.name, _itemAmount);
        }
    }
}