//this interface is contains the type of the item, 
//it has a use method that is invoked when the item is used
//equipped for equippables, or just normally used for consumeables
public interface IItemType
{

    //recieves the item as well as a struct with any context that would need to be modified
    //(pretty much just stats right now)
    public void Use(UseContext ctx); 

}