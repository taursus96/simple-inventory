
namespace SimpleInventory
{
    public interface IInventoryItemRenderer
    {
        void Setup(InventoryRenderer inventoryRenderer, IInventoryItem inventoryItem);
        IInventoryItem GetInventoryItem();
        IPosition GetNewPositionInInventory();
    }
}
