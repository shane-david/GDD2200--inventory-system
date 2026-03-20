public class NonStackable : IStackBehavior
{
    public bool TryToStack(ItemBase thisItem, ItemBase otherItem)
    {
        return false; 
    }
}