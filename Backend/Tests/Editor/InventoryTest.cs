using NUnit.Framework;
using SimpleInventory;

public class InventoryTest
{
	protected bool wasOnInventoryChangedCalled = false;

	[Test]
	public void CanPlace1x1ItemsOnValidSlotTest()
	{
		SimpleInventory.Inventory inventory = GetInventory10by10();

		ShouldBePlaced(GetOneByOneItem(), inventory, 1, 1);
		ShouldBePlaced(GetOneByOneItem(), inventory, 10, 1);
		ShouldBePlaced(GetOneByOneItem(), inventory, 1, 10);
		ShouldBePlaced(GetOneByOneItem(), inventory, 10, 10);
		ShouldBePlaced(GetOneByOneItem(), inventory, 5, 5);
	}

	[Test]
	public void CanPlace1x1ItemsAroundOtherItemsTest()
	{
		SimpleInventory.Inventory inventory = GetInventory10by10();

		ShouldBePlaced(GetOneByOneItem(), inventory, 5, 5);

		ShouldBePlaced(GetOneByOneItem(), inventory, 4, 4);
		ShouldBePlaced(GetOneByOneItem(), inventory, 4, 5);
		ShouldBePlaced(GetOneByOneItem(), inventory, 5, 4);
		ShouldBePlaced(GetOneByOneItem(), inventory, 5, 6);
		ShouldBePlaced(GetOneByOneItem(), inventory, 6, 5);
		ShouldBePlaced(GetOneByOneItem(), inventory, 6, 6);
	}

	[Test]
	public void CannotPlace1x1ItemsOnTopOfOtherItem()
	{
		SimpleInventory.Inventory inventory = GetInventory10by10();

		ShouldBePlaced(GetOneByOneItem(), inventory, 5, 5);

		ShouldntBePlaced(GetOneByOneItem(), inventory, 5, 5);
	}

	[Test]
	public void CanPlace2x2ItemsOnValidSlotTest()
	{
		SimpleInventory.Inventory inventory = GetInventory10by10();

		ShouldBePlaced(GetTwoByTwoItem(), inventory, 1, 1);
		ShouldBePlaced(GetTwoByTwoItem(), inventory, 9, 1);
		ShouldBePlaced(GetTwoByTwoItem(), inventory, 1, 9);
		ShouldBePlaced(GetTwoByTwoItem(), inventory, 9, 9);
		ShouldBePlaced(GetTwoByTwoItem(), inventory, 5, 5);
	}

	[Test]
	public void CanPlace2x2ItemsAroundOtherItemsTest()
	{
		SimpleInventory.Inventory inventory = GetInventory10by10();

		ShouldBePlaced(GetTwoByTwoItem(), inventory, 5, 5);

		ShouldBePlaced(GetTwoByTwoItem(), inventory, 3, 3);
		ShouldBePlaced(GetTwoByTwoItem(), inventory, 3, 5);
		ShouldBePlaced(GetTwoByTwoItem(), inventory, 5, 3);
		ShouldBePlaced(GetTwoByTwoItem(), inventory, 5, 7);
		ShouldBePlaced(GetTwoByTwoItem(), inventory, 7, 5);
		ShouldBePlaced(GetTwoByTwoItem(), inventory, 7, 7);
	}

	[Test]
	public void CannotPlace2x2ItemsOnTopOfOtherItem()
	{
		SimpleInventory.Inventory inventory = GetInventory10by10();

		ShouldBePlaced(GetTwoByTwoItem(), inventory, 5, 5);

		ShouldntBePlaced(GetTwoByTwoItem(), inventory, 5, 5);
		ShouldntBePlaced(GetTwoByTwoItem(), inventory, 6, 5);
		ShouldntBePlaced(GetTwoByTwoItem(), inventory, 5, 6);
		ShouldntBePlaced(GetTwoByTwoItem(), inventory, 6, 6);
	}

	[Test]
	public void CannotPlace1x1ItemsOutOfInventoryBoundaries()
	{
		SimpleInventory.Inventory inventory = GetInventory10by10();

		ShouldntBePlaced(GetOneByOneItem(), inventory, 0, 1);
		ShouldntBePlaced(GetOneByOneItem(), inventory, 1, 0);
		ShouldntBePlaced(GetOneByOneItem(), inventory, -1, 1);
		ShouldntBePlaced(GetOneByOneItem(), inventory, 1, -1);
		ShouldntBePlaced(GetOneByOneItem(), inventory, 11, 1);
		ShouldntBePlaced(GetOneByOneItem(), inventory, 1, 11);
		ShouldntBePlaced(GetOneByOneItem(), inventory, 11, 11);
	}

	[Test]
	public void CannotPlace2x2ItemsOutOfInventoryBoundaries()
	{
		SimpleInventory.Inventory inventory = GetInventory10by10();

		ShouldntBePlaced(GetTwoByTwoItem(), inventory, 0, 1);
		ShouldntBePlaced(GetTwoByTwoItem(), inventory, 1, 0);
		ShouldntBePlaced(GetTwoByTwoItem(), inventory, -1, 1);
		ShouldntBePlaced(GetTwoByTwoItem(), inventory, 1, -1);
		ShouldntBePlaced(GetTwoByTwoItem(), inventory, 11, 1);
		ShouldntBePlaced(GetTwoByTwoItem(), inventory, 1, 11);
		ShouldntBePlaced(GetTwoByTwoItem(), inventory, 11, 11);

		ShouldntBePlaced(GetTwoByTwoItem(), inventory, 10, 10);
		ShouldntBePlaced(GetTwoByTwoItem(), inventory, 9, 10);
		ShouldntBePlaced(GetTwoByTwoItem(), inventory, 10, 9);
	}

	[Test]
	public void CannotPlaceTheSameItemDataObjectMultipleTimes()
	{
		SimpleInventory.Inventory inventory = GetInventory10by10();
		IItemData sameItemData = GetOneByOneItem();

		ShouldBePlaced(sameItemData, inventory, 1, 1);
		ShouldntBePlaced(sameItemData, inventory, 5, 5);
	}

	[Test]
	public void CanItemBeDeletedFromInventory()
	{
		SimpleInventory.Inventory inventory = GetInventory10by10();
		IItemData itemData = GetTwoByTwoItem();

		ShouldBePlaced(itemData, inventory, 1, 1);

		Assert.AreEqual(1, inventory.GetItemsCount());
		
		IItemData deletedItem = inventory.DeleteItemOnPosition(1, 1);

		Assert.AreEqual(deletedItem, itemData);
		Assert.AreEqual(null, inventory.GetItemDataPlacedOnPosition(1, 1));
		Assert.AreEqual(0, inventory.GetItemsCount());
	}

	[Test]
	public void CanItemChangePosition()
	{
		SimpleInventory.Inventory inventory = GetInventory10by10();
		IItemData itemData = GetTwoByTwoItem();

		ShouldBePlaced(itemData, inventory, 1, 1);

		bool changed = inventory.ChangeItemPosition(1, 1, 5, 5);
		Assert.IsTrue(changed);
		Assert.AreEqual(1, inventory.GetItemsCount());

		Assert.AreEqual(itemData, inventory.GetItemDataPlacedOnPosition(5, 5));
		Assert.AreEqual(null, inventory.GetItemDataPlacedOnPosition(1, 1));
	}

	[Test]
	public void DoesItemStillExistIfChangingPositionFailed()
	{
		SimpleInventory.Inventory inventory = GetInventory10by10();
		IItemData itemData = GetTwoByTwoItem();

		ShouldBePlaced(itemData, inventory, 1, 1);

		bool changed = inventory.ChangeItemPosition(1, 1, -1, 5);
		Assert.IsFalse(changed);
		Assert.AreEqual(1, inventory.GetItemsCount());

		Assert.AreEqual(itemData, inventory.GetItemDataPlacedOnPosition(1, 1));
		Assert.AreEqual(null, inventory.GetItemDataPlacedOnPosition(-1, 5));
	}

	[Test]
	public void IsOnChangeCallbackCalledWhenPlacingItem()
	{
		wasOnInventoryChangedCalled = false;

		SimpleInventory.Inventory inventory = GetInventory10by10();
		inventory.AddOnChangedListener(OnInventoryChanged);
		IItemData itemData = GetTwoByTwoItem();

		ShouldBePlaced(itemData, inventory, 1, 1);

		Assert.AreEqual(wasOnInventoryChangedCalled, true);
	}

	[Test]
	public void IsOnChangeCallbackCalledWhenDeletingItem()
	{
		SimpleInventory.Inventory inventory = GetInventory10by10();
		inventory.AddOnChangedListener(OnInventoryChanged);
		IItemData itemData = GetTwoByTwoItem();

		ShouldBePlaced(itemData, inventory, 1, 1);

		wasOnInventoryChangedCalled = false;

		inventory.DeleteItemOnPosition(1, 1);

		Assert.AreEqual(wasOnInventoryChangedCalled, true);
	}

	[Test]
	public void IsOnChangeCallbackNotCalledWhenListenerIsRemoved()
	{
		wasOnInventoryChangedCalled = false;

		SimpleInventory.Inventory inventory = GetInventory10by10();
		inventory.AddOnChangedListener(OnInventoryChanged);
		inventory.RemoveOnChangedListener(OnInventoryChanged);
		IItemData itemData = GetTwoByTwoItem();

		ShouldBePlaced(itemData, inventory, 1, 1);

		Assert.AreEqual(wasOnInventoryChangedCalled, false);
	}

	public void OnInventoryChanged()
	{
		wasOnInventoryChangedCalled = true;
	}

	public void ShouldBePlaced(IItemData item, IInventory inventory, int posX, int posY)
	{
		bool placed = inventory.Place(item, posX, posY);
		Assert.IsTrue(placed);
		IItemData placedItem = inventory.GetItemDataPlacedOnPosition(posX, posY);
		Assert.AreEqual(placedItem, item);
	}

	public void ShouldntBePlaced(IItemData item, IInventory inventory, int posX, int posY)
	{
		bool placed = inventory.Place(item, posX, posY);
		Assert.IsFalse(placed);
		IItemData placedItem = inventory.GetItemDataPlacedOnPosition(posX, posY);
		Assert.AreNotEqual(placedItem, item);
	}


	protected SimpleInventory.Inventory GetInventory10by10()
	{
		SimpleInventory.Inventory inventory = new SimpleInventory.Inventory(10, 10);
		return inventory;
	}

	protected IItemData GetOneByOneItem()
	{
		IItemData itemData = new ItemData();

		itemData.SetSizeX(1);
		itemData.SetSizeY(1);

		return itemData;
	}

	protected IItemData GetTwoByTwoItem()
	{
		IItemData itemData = new ItemData();

		itemData.SetSizeX(2);
		itemData.SetSizeY(2);

		return itemData;
	}


}
