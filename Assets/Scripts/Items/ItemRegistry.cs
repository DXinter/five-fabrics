using System.Collections.Generic;

namespace Items
{
    public class ItemRegistry
    {
        private readonly Dictionary<ItemData, int> _items = new();

        public void AddItems(ItemData target, int count)
        {
            if (!_items.TryAdd(target, count))
            {
                _items[target] = +count;
            }
        }

        public int GetValue(ItemData key)
        {
            return _items.GetValueOrDefault(key, 0);
        }

        public Dictionary<ItemData, int> GetAllItems()
        {
            return _items;
        }
    }
}