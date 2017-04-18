using NUnit.Framework;
using SimpleInventory;
using System.Collections.Generic;

public class ItemDataTest
{
	[Test]
	public void ReturnsCorrectRequiredPositionsFor1x1()
	{
		IItemData itemOneByOne = new ItemData();
		itemOneByOne.SetSizeX(1);
		itemOneByOne.SetSizeY(1);

		List<IPosition> positions = itemOneByOne.GetRequiredPositions(1, 1);

		Assert.IsTrue(positions.Exists(p => 
			p.GetX() == 1 &&
			p.GetY() == 1));

		Assert.AreEqual(positions.Count, 1);
	}

	[Test]
	public void ReturnsCorrectRequiredPositionsFor2x2()
	{
		IItemData itemTwoByTwo = new ItemData();
		itemTwoByTwo.SetSizeX(2);
		itemTwoByTwo.SetSizeY(2);

		List<IPosition> positions = itemTwoByTwo.GetRequiredPositions(1, 1);

		Assert.IsTrue(positions.Exists(p => 
			p.GetX() == 1 &&
			p.GetY() == 1));

		Assert.IsTrue(positions.Exists(p => 
			p.GetX() == 2 &&
			p.GetY() == 1));

		Assert.IsTrue(positions.Exists(p => 
			p.GetX() == 1 &&
			p.GetY() == 2));

		Assert.IsTrue(positions.Exists(p => 
			p.GetX() == 2 &&
			p.GetY() == 2));

		Assert.AreEqual(positions.Count, 4);
	}
}