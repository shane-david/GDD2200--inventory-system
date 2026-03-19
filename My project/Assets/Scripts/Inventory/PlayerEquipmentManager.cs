using UnityEngine.Rendering;

public class PlayerEquipmentManager
{
    
    private Inventory _equipment = new("equipment", 5); 

    //-------
    //setters
    //-------

    public ItemBase SetHat(ItemBase hat)
    {
        
        //make sure it is a hat 
        if (hat.GetItemType() is EquipmentType equipment && equipment.EquipmentCategory == "hat")
        {   

            //get what was previously in the slot 
            var oldHat = _equipment.GetItem(0);

            //add the new hat to the slot
            _equipment.AddItem(hat, 0);  

            //return the oldhat
            return oldHat; 


        }

        return null; 

    }

    public ItemBase SetBackpack(ItemBase backpack)
    {
        
        //make sure it is a backpack
        if (backpack.GetItemType() is EquipmentType equipment && equipment.EquipmentCategory == "backpack")
        {
            
            //get what was previously in the slot 
            var oldBackpack = _equipment.GetItem(1);

            //ad the new backapck to the slot
            _equipment.AddItem(backpack, 1);

            //return the old backapck
            return oldBackpack; 
        }

        return null; 
    }

    public ItemBase SetBoots(ItemBase boots)
    {
        
        //make sure it is a backpack
        if (boots.GetItemType() is EquipmentType equipment && equipment.EquipmentCategory == "boot")
        {
            //get what was previously in the slot 
            var oldBoots = _equipment.GetItem(2);

            //ad the new backapck to the slot
            _equipment.AddItem(boots, 2);

            //return the old backapck
            return oldBoots; 
        }

        return null; 
    }

    public ItemBase SetAccessory(ItemBase accessory)
    {
        
        //make sure it is a backpack
        if (accessory.GetItemType() is EquipmentType equipment && equipment.EquipmentCategory == "accessory")
        {
            //get what was previously in the slot 
            var oldAccessory = _equipment.GetItem(3);

            //ad the new backapck to the slot
            _equipment.AddItem(accessory, 3);

            //return the old backapck
            return oldAccessory; 
        }

        return null; 
    }

    public ItemBase SetWeapon(ItemBase weapon)
    {
        
        //make sure it is a backpack
        if (weapon.GetItemType() is EquipmentType equipment && equipment.EquipmentCategory == "weapon")
        {
            //get what was previously in the slot 
            var oldWeapon = _equipment.GetItem(4);

            //ad the new backapck to the slot
            _equipment.AddItem(weapon, 4);

            //return the old backapck
            return oldWeapon; 
        }

        return null; 
    }

    //--------
    //getters
    //--------
    public Inventory GetEquipment() => _equipment; 
}