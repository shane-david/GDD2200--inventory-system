//class representing the consuemable type

using UnityEditor;

public class ConsumeableType : IItemType
{
    
    //string representing the stat to change when equippied
    private string StatToChange;

    //int representing how much to change the stat by 
    private int ChangeAmount;

    //constructor 
    public ConsumeableType(string statToChange, int changeAmount)
    {
        StatToChange = statToChange;
        ChangeAmount = changeAmount; 
    }

    public void Use(UseContext ctx)
    {
        
        if (ctx.item.quantity > 0) {
            //decrease the amount of the item
            ctx.item.quantity--; 

            //change the stat based off of what it is 
            switch (StatToChange)
            {
                case "health":
                    ctx.stats.changeHealth(ChangeAmount); 
                    break;
                case "attack":
                    ctx.stats.changeAttack(ChangeAmount);
                    break; 
                case "weight":
                    ctx.stats.changeWeight(ChangeAmount); 
                    break;
            }      
        } 

    }

}