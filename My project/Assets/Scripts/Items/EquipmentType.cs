//class representing the equipment type

public class EquipmentType : IItemType
{
    
    //string representing the category of eqiupment
    public string EquipmentCategory;

    //string representing the stat to change when equippied
    private string StatToChange;

    //int representing how much to change the stat by 
    private int ChangeAmount; 

    //constructor to set the equipment type
    public EquipmentType(string equipmentCat, string stat, int changeAmount)
    {
        EquipmentCategory = equipmentCat; 
        StatToChange = stat;
        ChangeAmount = changeAmount; 
    }

    //what happens when you use the equipment
    //increase stat based off of the stat chagne of the item
    public void Use(ItemBase item, UseContext ctx)
    {
        
    }

}