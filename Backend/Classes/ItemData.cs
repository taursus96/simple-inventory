using System.Collections.Generic;

namespace SimpleInventory
{
    public class ItemData : IItemData
    {
        protected int sizeX;
        protected int sizeY;

        public List<IPosition> GetRequiredPositions(int offsetX, int offsetY)
        {
            List<IPosition> requiredPositions = new List<IPosition>();

            for(int x = offsetX; x < offsetX + GetNumberOfInventoryHorizontalSlots(); x++)
            {
                for(int y = offsetY; y < offsetY + GetNumberOfInventoryVerticalSlots(); y++)
                {
                    requiredPositions.Add(new Position(x, y));
                }
            }

            return requiredPositions;
        }

        public int GetNumberOfInventoryHorizontalSlots()
        {
            return sizeX;
        }

        public int GetNumberOfInventoryVerticalSlots()
        {
            return sizeY;
        }

        public void SetSizeX(int sizeX)
        {
            this.sizeX = sizeX;
        }

        public void SetSizeY(int sizeY)
        {
            this.sizeY = sizeY;
        }
    }
}