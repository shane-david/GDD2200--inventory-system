using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;

public class PlayerInventory
{

    //ItemDatabase instance so that we can create items 
    ItemDatabase _itemDatabase; 

    //instances of Inventory representing the different sections of the player inventory
    Inventory _weapons; 
    Inventory _clothing; 

    //list representing the sections that are in the current tabe
    List<Inventory> _currentTab = new(); 

    //instance of equipment manager to keep track of the equipment tab
    PlayerEquipmentManager _equipment; 

    //-------------
    //constructors
    //-------------

    public PlayerInventory()
    {   
        //TODO base capacity (second argument) off of backpack
        _weapons  = new Inventory("Weapons", 5); 
        _clothing = new Inventory("Clothing", 5); 
        _itemDatabase = new ItemDatabase(); 
        BuildStartingItems(); 
    }

    //--------------
    //public methods
    //--------------

    //this method handles equipping
    //this is PlayerInventory wide as the equipment
    //panel is always visible 
    public bool Equip()
    {
        return false; 
    }

    //----------------
    //private 
    //----------------
    private void BuildStartingItems()
    {
        //starting items for weapons 
        _weapons.AddItem(_itemDatabase.CreatePocketKnife(), 2); 

        //starting items for clothing 
        _clothing.AddItem(_itemDatabase.CreateFishingHat(), 0);
        _clothing.AddItem(_itemDatabase.CreateLightBackpack(), 1); 
        _clothing.AddItem(_itemDatabase.CreateBoots(), 4);

    }

    //--------
    //getters
    //--------

    public Inventory GetWeaponSection() => _weapons; 

    public Inventory GetClothingSection() => _clothing; 

}
