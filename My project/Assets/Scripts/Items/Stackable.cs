public class Stackable : IStackBehavior
{
    
    public bool TryToStack(ItemBase thisItem, ItemBase otherItem)
    {
        
        if (thisItem.id == otherItem.id)
        {
            otherItem.quantity += thisItem.quantity;
            return true; 
        } 

        return false; 
    }
}