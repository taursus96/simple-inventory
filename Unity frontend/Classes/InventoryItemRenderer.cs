using UnityEngine;
using UnityEngine.EventSystems;
using SimpleInventory;
using UnityEngine.UI;

public class InventoryItemRenderer : MonoBehaviour, IInventoryItemRenderer, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	protected Vector3 dragOffset;
	protected InventoryRenderer inventoryRenderer;
	protected IInventoryItem inventoryItem;

	public void Setup(InventoryRenderer inventoryRenderer, IInventoryItem inventoryItem)
	{
		SetInventoryItem(inventoryItem);
		SetInventoryRenderer(inventoryRenderer);

		transform.SetParent(inventoryRenderer.transform);

		Image image = GetComponent<Image>();
		IItemDataGetSprite itemData = (IItemDataGetSprite)inventoryItem.GetItemData();
		image.sprite = itemData.GetSprite();

		RectTransform rectTransform = GetComponent<RectTransform>();
		int slotSizeInPx = inventoryRenderer.GetSlotSizeInPx();
		rectTransform.sizeDelta = GetNewSizeDelta(slotSizeInPx);
		rectTransform.anchoredPosition = GetNewAnchoredPosition(slotSizeInPx);
	}

	public void SetInventoryRenderer(InventoryRenderer inventoryRenderer)
	{
		this.inventoryRenderer = inventoryRenderer;
	}

	public InventoryRenderer GetInventoryRenderer()
	{
		return inventoryRenderer;
	}

	public void SetInventoryItem(IInventoryItem inventoryItem)
	{
		this.inventoryItem = inventoryItem;
	}

	public IInventoryItem GetInventoryItem()
	{
		return inventoryItem;
	}

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragOffset = transform.position - Input.mousePosition;
		transform.SetParent(inventoryRenderer.GetInventoryOverlay());
		GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition + dragOffset;
    }

	public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;

		if(transform.parent == inventoryRenderer.GetInventoryOverlay())
		{
			OnDropOutsideOfInventory();
		}
    }

	protected void OnDropOutsideOfInventory()
	{
		inventoryRenderer.GetInventory().DeleteItemOnPosition(inventoryItem.GetPosX(), inventoryItem.GetPosY());
	}

    public IPosition GetNewPositionInInventory()
    {
		RectTransform rectTransform = GetComponent<RectTransform>();

		Position newPosition = new Position(
			(int)Mathf.Floor((rectTransform.anchoredPosition.x + inventoryRenderer.slotSizeInPx / 2)  / inventoryRenderer.slotSizeInPx) + 1,
			(int)Mathf.Floor((-rectTransform.anchoredPosition.y + inventoryRenderer.slotSizeInPx / 2) / inventoryRenderer.slotSizeInPx) + 1
		);

		return newPosition;
    }

	public Vector2 GetNewSizeDelta(int slotSizeInPx)
	{
		IItemData itemData = inventoryItem.GetItemData();
		return new Vector2(
			slotSizeInPx * itemData.GetNumberOfInventoryHorizontalSlots(), 
			slotSizeInPx * itemData.GetNumberOfInventoryVerticalSlots()
		);
	}

	public Vector2 GetNewAnchoredPosition(int slotSizeInPx)
	{
		return new Vector2(
			(inventoryItem.GetPosX() - 1) * slotSizeInPx,
			-(inventoryItem.GetPosY() - 1) * slotSizeInPx
		);
	}
}
