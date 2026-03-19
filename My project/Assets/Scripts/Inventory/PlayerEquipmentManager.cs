using UnityEngine.Rendering;

public class PlayerEquipmentManager
{
    
    private Inventory _equipment = new("equipment", 5); 

    //-------
    //setters
    //-------

    public void SetHat(ItemBase hat)
    {
        
        //make sure it is a hat 
        if (hat.GetItemType() is EquipmentType equipment && equipment.EquipmentCategory == "hat")
        {
            _equipment.AddItem(hat, 0); 
        }

    }

    public void SetBackpack(ItemBase backpack)
    {
        
        //make sure it is a backpack
        if (backpack.GetItemType() is EquipmentType equipment && equipment.EquipmentCategory == "backpack")
        {
            _equipment.AddItem(backpack, 1); 
        }
    }

    public void SetBoots(ItemBase boots)
    {
        
        //make sure it is a backpack
        if (boots.GetItemType() is EquipmentType equipment && equipment.EquipmentCategory == "boot")
        {
            _equipment.AddItem(boots, 2); 
        }
    }

    public void SetAccessory(ItemBase accessory)
    {
        
        //make sure it is a backpack
        if (accessory.GetItemType() is EquipmentType equipment && equipment.EquipmentCategory == "accessory")
        {
            _equipment.AddItem(accessory, 3); 
        }
    }

    public void SetWeapon(ItemBase weapon)
    {
        
        //make sure it is a backpack
        if (weapon.GetItemType() is EquipmentType equipment && equipment.EquipmentCategory == "weapon")
        {
            _equipment.AddItem(weapon, 4); 
        }
    }

    //--------
    //getters
    //--------
    public Inventory GetEquipment() => _equipment; 
}