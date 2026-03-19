using System.Collections.Generic;
using UnityEngine; 

public class PlayerInventory
{

    //ItemDatabase instance so that we can create items 
    ItemDatabase _itemDatabase; 

    //instances of Inventory representing the different sections of the player inventory
    Inventory _weapons; 
    Inventory _clothing; 
    Inventory _accessories; 

    //list representing the sections that are in the current tabe
    List<Inventory> _currentTab = new(); 

    //instance of equipment manager to keep track of the equipment tab
    PlayerEquipmentManager _equipmentManager = new(); 

    //instance of the stats manager to keep track of the stats tab
    PlayerStatsMangager _statsManager; 

    //use context to pass into the use methods for items
    UseContext useCtx; 

    //-------------
    //constructors
    //-------------

    public PlayerInventory(PlayerStatsMangager stats)
    {   
        //TODO base capacity (second argument) off of backpack
        _weapons  = new Inventory("Weapons", 5); 
        _clothing = new Inventory("Clothing", 5); 
        _accessories = new Inventory("Tools", 15); 
        _itemDatabase = new ItemDatabase(); 
        BuildStartingItems(); 

        //instantiate the stats manager 
        _statsManager = stats; 

        //build the use ctx
        useCtx = new() {
            stats = _statsManager
        };
            
        
    }

    //--------------
    //public methods
    //--------------

    //this method handles equipping this is PlayerInventory wide as the equipment panel is always visible 
    public bool Equip(Inventory prevSection, int prevIndex, int equipIndex)
    {   

        //boolean representing the result of the equip
        bool result = false; 

        //get the item type interface from the item
        EquipmentType itemType = prevSection.GetItem(prevIndex).GetItemType() as EquipmentType; 

        //ERROR checking, make sure the item is equipment type
        if (itemType != null)
        {
            
            //ERROR checking, make sure the equipment type category matches the equip index
            //0 -> hat 1 -> backpack 2-> boot 3-> accessory 4 -> weapon
            if (itemType.EquipmentCategory == "hat" && equipIndex == 0)
            {   

                //get the hat in the equipment manager set this equal to the other hat so 
                //we know what was in the slot previously 
                var otherHat = _equipmentManager.SetHat(prevSection.GetItem(prevIndex)); 

                //set the index of that inventory section to the other hat, it will be null already if there was nothing there 
                prevSection.AddItem(otherHat, prevIndex); 

                result = true; 

            } else if (itemType.EquipmentCategory == "backpack" && equipIndex == 1) {
                
                //get the backpack in the quipmenager, set this equal to the other backpack
                //so that we know what was in the slot previoulsy 
                var otherBackpack = _equipmentManager.SetBackpack(prevSection.GetItem(prevIndex));

                //set the index of that inventory section to the other backpack, it will be null already if there was nothign there
                prevSection.AddItem(otherBackpack, prevIndex);

                result = true; 

            } else if (itemType.EquipmentCategory == "boot" && equipIndex == 2)
            {

               //get the boots in the quipmenager, set this equal to the other boots
                //so that we know what was in the slot previoulsy 
                var otherBoots = _equipmentManager.SetBoots(prevSection.GetItem(prevIndex));

                //set the index of that inventory section to the other backpack, it will be null already if there was nothign there
                prevSection.AddItem(otherBoots, prevIndex);

                result = true;  

            } else if (itemType.EquipmentCategory == "accessory" && equipIndex == 3)
            {
                
               //get the accessory in the quipmenager, set this equal to the other accessory
                //so that we know what was in the slot previoulsy 
                var otherAccessory = _equipmentManager.SetAccessory(prevSection.GetItem(prevIndex));

                //set the index of that inventory section to the other backpack, it will be null already if there was nothign there
                prevSection.AddItem(otherAccessory, prevIndex);

                result = true; 

            } else if (itemType.EquipmentCategory == "weapon" && equipIndex == 4)
            {
               //get the weapon in the equipmenager, set this equal to the other weapon
                //so that we know what was in the slot previoulsy 
                var otherWeapon = _equipmentManager.SetWeapon(prevSection.GetItem(prevIndex));

                //set the index of that inventory section to the other backpack, it will be null already if there was nothign there
                prevSection.AddItem(otherWeapon, prevIndex);

                result = true; 

            } else 
            {
                result = false;
            }
        } else
        {
            result = false; 
        }

        //if the result was a successful equip call use on the equipment 
        if (result)
        {
            itemType.Use(useCtx); 
        }

        //return the result
        return result; 
 
    }

    //this method handles unequipping, this is PlayerInventory wide as the equipment panel is always visible
    public bool Unequip(int prevIndex, Inventory newSection, int newIndex)
    {
        
        //get the item type from the item type interface 
        //NOTE it will always be equipment type if this method is called
        EquipmentType itemType = _equipmentManager.GetEquipment().GetItem(prevIndex).GetItemType() as EquipmentType; 

        //ERROR checking, if it is a hat, backpack, or boot item it has to go to the clothing section 
        //or if it is an accessory it has to go in the Tools section 
        //or if it a weapon it has to go in the weapons section
        if (((itemType.EquipmentCategory == "hat" || itemType.EquipmentCategory == "boot" || itemType.EquipmentCategory == "backpack") && newSection.GetName() == "Clothing") ||
            (itemType.EquipmentCategory == "accessory" && newSection.GetName() == "Tools") ||
            (itemType.EquipmentCategory == "weapon" && newSection.GetName() == "Weapons")
        )
        {
            
            //get the item that is in the new index slot 
            ItemBase newItem = newSection.GetItem(newIndex); 

            //if there is nothing in that slot (it is null), simply add the item to the inventory 
            if (newItem == null) {

                //add the item back to the new index in the inventory 
                newSection.AddItem(_equipmentManager.GetEquipment().GetItem(prevIndex), newIndex); 

                //remove the hat from the equipment inventory
                _equipmentManager.GetEquipment().RemoveItem(prevIndex); 

                //unequip it in data 
                itemType.Unequip(useCtx); 
                return true; 

            //otherwise, there is in item in that inventory 
            } else {

                //if the item in that inventory matches the equipment type of what is being unequippied swap them
                if (newItem.GetItemType() is EquipmentType newType && newType.EquipmentCategory == itemType.EquipmentCategory) {
                    
                    //add the old item to its inventory slot
                    newSection.AddItem(_equipmentManager.GetEquipment().GetItem(prevIndex), newIndex); 

                    //add the new item to its respective equipment slot 
                    switch (prevIndex)
                    {
                        case 0:
                            _equipmentManager.SetHat(newItem);
                            break;
                        case 1:
                            _equipmentManager.SetBackpack(newItem);
                            break;
                        case 2:
                            _equipmentManager.SetBoots(newItem); 
                            break;
                        case 3:
                            _equipmentManager.SetAccessory(newItem);
                            break;
                        case 4:
                            _equipmentManager.SetWeapon(newItem);
                            break;
                    }

                    return true; 

                //otherwise you can not unequip so return false for a snap back
                } else {
                    return false; 
                }
            }

        }

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
        _clothing.AddItem(_itemDatabase.CreateCowboyHat(), 2); 

        //starting items for accessories
        _accessories.AddItem(_itemDatabase.CreateRope(), 0);
        _accessories.AddItem(_itemDatabase.CreateFishingRod(), 12);
        _accessories.AddItem(_itemDatabase.CreateFlashlight(), 4); 

    }

    //--------
    //getters
    //--------

    public Inventory GetWeaponSection() => _weapons; 

    public Inventory GetClothingSection() => _clothing; 

    public Inventory GetAccessorySection() => _accessories; 

    public Inventory GetEquipmentSection()
    {
        return _equipmentManager.GetEquipment(); 
    }

}
