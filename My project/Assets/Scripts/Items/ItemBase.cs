using UnityEngine;

public class ItemBase
{
  
    //public variable representing the attributes items
    public string name;
    public string description;
    public string flavorText; 
    public string id;
    public int quantity; 

    //Interface representing the type of the item 
    private IItemType _itemType; 

    //Interface representing the stacking behavior of the item
    private IStackBehavior _stackBehavior; 

    //-----------------------------------------
    //constructor to build each individual item 
    //-----------------------------------------

    //TODO add item type and stack behavior 
    public ItemBase(string name, string description, string flavorText, string id, int quantity, IItemType itemType, IStackBehavior stackBehavior)
    {
        this.name = name;
        this.description = description;
        this.flavorText = flavorText; 
        this.id = id;
        this.quantity = quantity;  
        _itemType = itemType; 
        _stackBehavior = stackBehavior; 
    }

    //--------
    //getters
    //--------
    public IItemType GetItemType() => _itemType; 
    public IStackBehavior GetStackBehavior() => _stackBehavior; 
}
