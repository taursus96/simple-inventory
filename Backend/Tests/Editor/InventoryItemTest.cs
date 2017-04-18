using NUnit.Framework;
using SimpleInventory;

public class InventoryItemTest
{
	[Test]
	public void ReturnsCorrectlyIfIsOccupyingPositionFor1x1()
	{
		IItemData itemOneByOne = new ItemData();
		itemOneByOne.SetSizeX(1);
		itemOneByOne.SetSizeY(1);

		IInventoryItem item = new InventoryItem(itemOneByOne, 1, 1);

		Assert.IsTrue(item.IsOccupyingPosition(1, 1));

		Assert.IsFalse(item.IsOccupyingPosition(2, 1));
		Assert.IsFalse(item.IsOccupyingPosition(1, 2));
		Assert.IsFalse(item.IsOccupyingPosition(2, 2));
		Assert.IsFalse(item.IsOccupyingPosition(0, 1));
		Assert.IsFalse(item.IsOccupyingPosition(-1, 1));
	}

	[Test]
	public void ReturnsCorrectlyIfIsOccupyingPositionFor2x2()
	{
		IItemData itemOneByOne = new ItemData();
		itemOneByOne.SetSizeX(2);
		itemOneByOne.SetSizeY(2);

		IInventoryItem item = new InventoryItem(itemOneByOne, 5, 5);

		Assert.IsTrue(item.IsOccupyingPosition(5, 5));
		Assert.IsTrue(item.IsOccupyingPosition(5, 6));
		Assert.IsTrue(item.IsOccupyingPosition(6, 5));
		Assert.IsTrue(item.IsOccupyingPosition(6, 6));

		Assert.IsFalse(item.IsOccupyingPosition(-1, 1));
		Assert.IsFalse(item.IsOccupyingPosition(4, 4));
		Assert.IsFalse(item.IsOccupyingPosition(4, 5));
		Assert.IsFalse(item.IsOccupyingPosition(5, 4));
		Assert.IsFalse(item.IsOccupyingPosition(7, 7));
		Assert.IsFalse(item.IsOccupyingPosition(3, 7));
		Assert.IsFalse(item.IsOccupyingPosition(5, 7));
		Assert.IsFalse(item.IsOccupyingPosition(6, 7));
	}
}