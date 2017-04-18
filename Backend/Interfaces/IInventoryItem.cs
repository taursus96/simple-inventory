namespace SimpleInventory
{
    public interface IInventoryItem
    {
        IItemData GetItemData();
        int GetPosX();
        int GetPosY();

        bool IsOccupyingPosition(int posX, int posY);
    }
}
