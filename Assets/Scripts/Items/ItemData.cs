using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Item Data")]
    public class ItemData : ScriptableObject
    {
        public Sprite avatar;
        public new string name;
    }
}