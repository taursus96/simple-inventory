using System;
using System.Collections.Generic;

namespace SimpleInventory
{
    public class Inventory : IInventory
    {
        protected int sizeX;
        protected int sizeY;

        protected List<IInventoryItem> items;

        protected event Action onInventoryChanged = delegate {};

        public Inventory(int sizeX, int sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;

            items = new List<IInventoryItem>();
        }

        public IInventoryItem GetInventoryItemPlacedOnPosition(int posX, int posY)
        {
            return items.Find(x => 
                x.GetPosX() == posX && 
                x.GetPosY() == posY);
        }

        public IItemData GetItemDataPlacedOnPosition(int posX, int posY)
        {
            IInventoryItem item = GetInventoryItemPlacedOnPosition(posX, posY);
            return item == null ? null : item.GetItemData();
        }

        public bool CanPlace(IItemData itemData, int posX, int posY)
        {
            List<IPosition> requiredPositions = itemData.GetRequiredPositions(posX, posY);
            bool isAnyRequiredPositionOccupied = requiredPositions.Exists(pos => IsPositionOccupied(pos.GetX(), pos.GetY()));
            bool isAnyRequiredPositionOutOfBoundaries = requiredPositions.Exists(pos => IsPositionOutOfBoundaries(pos.GetX(), pos.GetY()));
            bool isThisItemDataObjectAlreadyPlaced = items.Exists(item => item.GetItemData() == itemData);
            
            return !isAnyRequiredPositionOutOfBoundaries && 
                !isAnyRequiredPositionOccupied && 
                !isThisItemDataObjectAlreadyPlaced;
        }

        public bool IsPositionOutOfBoundaries(int posX, int posY)
        {
            if(posX < 1 || posY < 1 || posX > this.sizeX || posY > this.sizeY)
            {
                return true;
            }

            return false;
        }

        public bool IsPositionOccupied(int posX, int posY)
        {
           return items.Exists(item => item.IsOccupyingPosition(posX, posY));
        }

        public bool Place(IItemData itemData, int posX, int posY)
        {
            if(CanPlace(itemData, posX, posY))
            {
                InventoryItem item = new InventoryItem(itemData, posX, posY);
                items.Add(item);
                onInventoryChanged.Invoke();
                return true;
            }

            return false;
        }

        public IItemData DeleteItemOnPosition(int posX, int posY)
        {
            IInventoryItem inventoryItem = GetInventoryItemPlacedOnPosition(posX, posY);
            items.Remove(inventoryItem);
            onInventoryChanged.Invoke();
            return inventoryItem.GetItemData();
        }

        public bool ChangeItemPosition(int currentPosX, int currentPosY, int newPosX, int newPosY)
        {
            IItemData itemData = DeleteItemOnPosition(currentPosX, currentPosY);
            bool wasPlaced = Place(itemData, newPosX, newPosY);

            if(!wasPlaced)
            {
                Place(itemData, currentPosX, currentPosY);
                return false;
            }
            
            return true;
        }

        public int GetItemsCount()
        {
            return items.Count;
        }

        public void AddOnChangedListener(Action action)
        {
            onInventoryChanged += action;
        }

        public void RemoveOnChangedListener(Action action)
        {
            onInventoryChanged -= action;
        }

        public List<IInventoryItem> GetItems()
        {
            return items;
        }

        public bool TransferItemToInventory(int currentPosX, int currentPosY, int newPosX, int newPosY, IInventory newInventory)
        {
            if(newInventory == this)
            {
                ChangeItemPosition(currentPosX, currentPosY, newPosX, newPosY);
            }
            else
            {
                IItemData itemData = DeleteItemOnPosition(currentPosX, currentPosY);
                bool wasPlaced = newInventory.Place(itemData, newPosX, newPosY);

                if(!wasPlaced)
                {
                    Place(itemData, currentPosX, currentPosY);
                    return false;
                }
            }

            return true;
        }
    }
}