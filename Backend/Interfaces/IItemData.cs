using System.Collections.Generic;

namespace SimpleInventory
{
	public interface IItemData
	{
		void SetSizeX(int sizeX);
		void SetSizeY(int sizeY);
		int GetNumberOfInventoryHorizontalSlots();
		int GetNumberOfInventoryVerticalSlots();
		List<IPosition> GetRequiredPositions(int offsetX, int offsetY);
	}
}