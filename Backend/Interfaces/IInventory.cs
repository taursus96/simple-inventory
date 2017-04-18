using System;
using System.Collections.Generic;

namespace SimpleInventory
{
	public interface IInventory
	{
		bool Place(IItemData itemData, int posX, int posY);
		IItemData GetItemDataPlacedOnPosition(int posX, int posY);
		IItemData DeleteItemOnPosition(int posX, int posY);
		bool ChangeItemPosition(int currentPosX, int currentPosY, int newPosX, int newPosY);
		bool TransferItemToInventory(int currentPosX, int currentPosY, int newPosX, int newPosY, IInventory inventory);
		int GetItemsCount();
		void AddOnChangedListener(Action action);
		void RemoveOnChangedListener(Action action);
		List<IInventoryItem> GetItems();
	}
}