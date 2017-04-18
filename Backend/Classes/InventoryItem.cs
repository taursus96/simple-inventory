using System.Collections.Generic;

namespace SimpleInventory
{
    public class InventoryItem : IInventoryItem
    {
        protected IItemData itemData;
        protected int posX;
        protected int posY;

        public InventoryItem(IItemData itemData, int posX, int posY)
        {
            this.itemData = itemData;
            this.posX = posX;
            this.posY = posY;
        }
        
        public IItemData GetItemData()
        {
            return itemData;
        }

        public int GetPosX()
        {
            return posX;
        }

        public int GetPosY()
        {
            return posY;
        }

        public bool IsOccupyingPosition(int posX, int posY)
        {
            List<IPosition> occupiedPosititons = itemData.GetRequiredPositions(this.posX, this.posY);
            return occupiedPosititons.Exists(p => 
                p.GetX() == posX && p.GetY() == posY);
        }
    }
}