using UnityEngine;

namespace SimpleInventory
{
    [CreateAssetMenu(fileName = "Data", menuName = "Inventory/ItemScriptable", order = 1)]
    public class ItemScriptable : ScriptableObject
    {
        public int sizeX;
        public int sizeY;
        public Sprite sprite;
        public string name;
    }
}