using System.Collections.Generic;
using System.Diagnostics;

public class Inventory
{
    //private variables specific to the type of grid
    private string _sectionName; 
    private int    _capacity; 
    
    //list of all the items in this specific tab
    private List<ItemBase> _items; 

    //-------------
    //constructors
    //-------------
    public Inventory(string sectionName, int capacity)
    {
        _sectionName = sectionName; 
        _capacity    = capacity; 

        //fill the list with null 
        _items = new List<ItemBase>(new ItemBase[capacity]); 
    }

    //--------------
    //public methods
    //--------------


    //add an item to the list at a certian spot 
    //returns true if it was added successfully 
    //TODO item overlap checking and bounds checking 
    public bool AddItem(ItemBase item, int index)
    {
        _items[index] = item; 
        return true; 
    }

    //removes an item from the items list, returns whether or not
    //removing the item was successful 
    //TODO bounds checking
    public void RemoveItem(int index)
    {
        _items[index] = null; 
    }

    //moves an item from one index to another index
    //returns whether or not the swap was successful
    public bool SwapItem(int currentIndex, int newIndex)
    {
        var temp = _items[currentIndex]; 
        _items[currentIndex] = _items[newIndex];
        _items[newIndex] = temp; 
        return true; 
    }

    //--------
    //getters
    //--------

    public ItemBase GetItem(int index)
    {
        return _items[index]; 
    }

    public List<ItemBase> GetAllItems()
    {
        return _items; 
    }

    public string GetName() => _sectionName; 
}