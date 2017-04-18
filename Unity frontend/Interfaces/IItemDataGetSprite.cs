using UnityEngine;

namespace SimpleInventory
{
    public interface IItemDataGetSprite : IItemData
    {
        Sprite GetSprite();
    }
}