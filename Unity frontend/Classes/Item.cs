using System;
using UnityEngine;

namespace SimpleInventory
{
    public class Item : ItemData, IItemDataGetSprite
    {
        protected Sprite sprite;
        protected string name;

        public void SetSprite(Sprite sprite)
        {
            this.sprite = sprite;
        }

        public Sprite GetSprite()
        {
            return sprite;
        }

        public void SetName(String name)
        {
            this.name = name;
        }

        public String GetName()
        {
            return name;
        }

        static public Item CreateFrom(ItemScriptable itemScriptable)
        {
            Item item = new Item();
            item.SetSizeX(itemScriptable.sizeX);
            item.SetSizeY(itemScriptable.sizeY);
            item.SetName(itemScriptable.name);
            item.SetSprite(itemScriptable.sprite);
            return item;
        }
    }
}