using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.SceneManagement;

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
    PlayerEquipmentManager _equipmentManager = new(); 

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

    //this method handles equipping this is PlayerInventory wide as the equipment panel is always visible 
    public bool Equip(Inventory prevSection, int prevIndex, int equipIndex)
    {   

        //get the item type interface from the item
        EquipmentType itemType = prevSection.GetItem(prevIndex).GetItemType() as EquipmentType; 

        //ERROR checking, make sure the item is equipment type
        if (itemType != null)
        {
            
            //ERROR checking, make sure the equipment type category matches the equip index
            //0 -> hat 1 -> backpack 2-> boot 3-> accessory 4 -> weapon
            if (itemType.EquipmentCategory == "hat" && equipIndex == 0)
            {   
                
                //TODO if something already exists there swap it 

                //set the hat in the equipment manager
                _equipmentManager.SetHat(prevSection.GetItem(prevIndex)); 

                //set the index of that inventory section to null
                prevSection.RemoveItem(prevIndex); 

                return true; 

            } else if (itemType.EquipmentCategory == "backpack" && equipIndex == 1)
            {
                
                //TODO if something aleady exists there swap it

                //set the backapck in the equipment manager
                _equipmentManager.SetBackpack(prevSection.GetItem(prevIndex)); 

                //set the index of that inventory section to null
                prevSection.RemoveItem(prevIndex);

                return true; 

            } else if (itemType.EquipmentCategory == "boot" && equipIndex == 2)
            {

                //TODO if something aleady exists there swap it

                //set the backapck in the equipment manager
                _equipmentManager.SetBoots(prevSection.GetItem(prevIndex)); 

                //set the index of that inventory section to null
                prevSection.RemoveItem(prevIndex);

                return true;   

            } else if (itemType.EquipmentCategory == "accessory" && equipIndex == 3)
            {
                
                //TODO if something aleady exists there swap it

                //set the backapck in the equipment manager
                _equipmentManager.SetAccessory(prevSection.GetItem(prevIndex)); 

                //set the index of that inventory section to null
                prevSection.RemoveItem(prevIndex);

                return true; 

            } else if (itemType.EquipmentCategory == "weapon" && equipIndex == 4)
            {
                
                //TODO if something aleady exists there swap it

                //set the backapck in the equipment manager
                _equipmentManager.SetWeapon(prevSection.GetItem(prevIndex)); 

                //set the index of that inventory section to null
                prevSection.RemoveItem(prevIndex);

                return true; 

            } else 
            {
                return false;
            }
        }

        return false; 
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
            
            //TODO if something already exists there swap it if able 

            //add the item back to the new index in the inventory 
            newSection.AddItem(_equipmentManager.GetEquipment().GetItem(prevIndex), newIndex); 

            //remove the hat from the equipment inventory
            _equipmentManager.GetEquipment().RemoveItem(prevIndex); 

            return true; 

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

    }

    //--------
    //getters
    //--------

    public Inventory GetWeaponSection() => _weapons; 

    public Inventory GetClothingSection() => _clothing; 

    public Inventory GetEquipmentSection()
    {
        return _equipmentManager.GetEquipment(); 
    }

}
