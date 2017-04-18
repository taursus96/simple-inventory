using SimpleInventory;
using UnityEngine;

public class InventoryRendererInjector : MonoBehaviour
{
	public ItemScriptable item;

	void Start ()
	{
		IInventory inventory = new Inventory(8, 8);
		InventoryRenderer renderer = GetComponent<InventoryRenderer>();
		renderer.SetInventory(inventory);

		inventory.Place(Item.CreateFrom(item), 1, 1);
		inventory.Place(Item.CreateFrom(item), 4, 1);
		inventory.Place(Item.CreateFrom(item), 2, 4);
		inventory.Place(Item.CreateFrom(item), 6, 7);
	}
}
