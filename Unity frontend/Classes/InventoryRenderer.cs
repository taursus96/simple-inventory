using UnityEngine;
using SimpleInventory;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class InventoryRenderer : MonoBehaviour, IDropHandler
{
	public GameObject itemRendererPrefab;
	public Transform inventoryOverlay;
	public int slotSizeInPx = 32;
	protected IInventory inventory;
	protected List<GameObject> itemRenderersInUse;
	static protected Stack<GameObject> itemRenderersPool = new Stack<GameObject>();

	void Awake()
	{
		itemRenderersInUse = new List<GameObject>();
	}

	public void SetInventory(IInventory inventory)
	{
		if(this.inventory != null)
		{
			this.inventory.RemoveOnChangedListener(OnInventoryChanged);
		}

		this.inventory = inventory;
		inventory.AddOnChangedListener(OnInventoryChanged);
	}

	public IInventory GetInventory()
	{
		return inventory;
	}

	public Transform GetInventoryOverlay()
	{
		return inventoryOverlay;
	}

	public int GetSlotSizeInPx()
	{
		return slotSizeInPx;
	}

	protected void OnInventoryChanged()
	{
		Render();
	}

	public void Render()
	{
		FreeItemRenderersInUse();
		RenderItems();
	}

	protected void FreeItemRenderersInUse()
	{
		itemRenderersInUse.ForEach(itemRenderer => { 
			itemRenderer.SetActive(false); 
			itemRenderersPool.Push(itemRenderer);
		});
		itemRenderersInUse.Clear();
	}

	protected void RenderItems()
	{
		inventory.GetItems().ForEach(item => RenderItem(item));
	}

	protected GameObject CreateNewItemRenderer()
	{
		return (GameObject)Instantiate(itemRendererPrefab);
	}

	protected GameObject GetUnusedItemRenderer()
	{
		GameObject itemRenderer = itemRenderersPool.Count > 0 ? itemRenderersPool.Pop() : CreateNewItemRenderer();
		itemRenderer.SetActive(true);
		itemRenderersInUse.Add(itemRenderer);
		return itemRenderer;
	}

	protected void RenderItem(IInventoryItem item)
	{
		GetUnusedItemRenderer().GetComponent<InventoryItemRenderer>().Setup(this, item);
	}

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
		{
			InventoryItemRenderer inventoryItemRenderer = eventData.pointerDrag.GetComponent<InventoryItemRenderer>();
			
			if(inventoryItemRenderer != null)
			{
				inventoryItemRenderer.transform.SetParent(transform);
				IInventoryItem inventoryItem = inventoryItemRenderer.GetInventoryItem();
				IPosition newPositionInInventory = inventoryItemRenderer.GetNewPositionInInventory();

				inventoryItemRenderer.GetInventoryRenderer().GetInventory().TransferItemToInventory(
					inventoryItem.GetPosX(),
					inventoryItem.GetPosY(),
					newPositionInInventory.GetX(),
					newPositionInInventory.GetY(),
					inventory
				);
			}
		}
    }
}
