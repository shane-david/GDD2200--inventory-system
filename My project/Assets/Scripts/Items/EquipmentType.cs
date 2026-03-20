//class representing the equipment type

public class EquipmentType : IItemType
{
    
    //string representing the category of eqiupment
    public string EquipmentCategory;

    //string representing the stat to change when equippied
    private string StatToChange;

    //int representing how much to change the stat by 
    private int ChangeAmount; 

    //weight of equipment to change the weight when an item is held by the player
    private int Weight; 

    //constructor to set the equipment type
    public EquipmentType(string equipmentCat, string stat, int changeAmount, int weight)
    {
        EquipmentCategory = equipmentCat; 
        StatToChange = stat;
        ChangeAmount = changeAmount; 
        Weight = weight; 
    }

    //what happens when you use the equipment
    //increase stat based off of the stat chagne of the item
    public void Use(UseContext ctx)
    {
        ChangeStats(ctx, 1); 
    }

    //called when unequipped, does the same thing as the use but negative
    public void Unequip(UseContext ctx)
    {
        ChangeStats(ctx, -1); 
    }

    //changes stats based off passed in direction
    public void ChangeStats(UseContext ctx, int direction)
    {
        //change the stat based off of what it is 
        switch (StatToChange)
        {
            case "health":
                ctx.stats.changeHealth(ChangeAmount*direction); 
                break;
            case "attack":
                ctx.stats.changeAttack(ChangeAmount*direction);
                break; 
            case "weight":
                ctx.stats.changeWeight(ChangeAmount*direction); 
                break;
        }      

        //alawys change the wieght
        ctx.stats.changeWeight(Weight*direction); 
    }


}