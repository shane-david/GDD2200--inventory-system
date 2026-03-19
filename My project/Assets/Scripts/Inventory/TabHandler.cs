using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TabHandler : MonoBehaviour
{
    
    //private variable is an instance to the player inventory so 
    //that the UIHandler can use that info to display it 
    private PlayerInventory _playerInventory; 

    //assigned in the inspector, to get the slot handler for each section of the inventory
    [Header("Drag in each slot section for each tab")]
    [SerializeField] private SlotHandler _weaponSlots;
    [SerializeField] private SlotHandler _clothingSlots;
    [SerializeField] private SlotHandler _equipmentSlots; 

    //dictionary representing the current tab, contains 
    //key of Inventories in that tab to their respective slot handlers
    private Dictionary<Inventory, SlotHandler> _currentTab = new(); 

    //dictionary from strings to all of the sections so that the tab handler
    //knows wich tab to call the SwapItems method on 
    private Dictionary<string, Inventory> _allSections = new();

    //refernce to the player object for stats
    [SerializeField] private GameObject _player;  

    //----------------------
    //Unity Lifetime Methods
    //----------------------

    //build the dictionary for the gear tab as thats what the player will start on
    private void Awake()
    {   
        //instantiate player inventory
        _playerInventory = new PlayerInventory(_player.GetComponent<PlayerStatsMangager>()); 

        //build the sections dictionary  
        _allSections.Add("weapons", _playerInventory.GetWeaponSection());
        _allSections.Add("clothing", _playerInventory.GetClothingSection()); 
        _allSections.Add("equipment", _playerInventory.GetEquipmentSection()); 

    }


    //---------------
    //public methods
    //---------------
    public void ChangeTab(string tabName)
    {   

        _currentTab.Clear(); 

        switch (tabName)
        {   
            //weapons and clothing section within the gear tab 
            case "gear": 
                _currentTab.Add(_playerInventory.GetWeaponSection(), _weaponSlots);
                _currentTab.Add(_playerInventory.GetClothingSection(), _clothingSlots);  
                break; 
        }

        //always add equipment 
        _currentTab.Add(_playerInventory.GetEquipmentSection(), _equipmentSlots); 
    }

    //this method uses the passed in section to find the inventory 
    //in the dictionary and then calls the wap item method on that
    public void SwapItems(int originalIndex, int newIndex, string section)
    {
        _allSections[section].SwapItem(originalIndex, newIndex); 
    }

    //this equips the item to the player inventory 
    //it passes in the previous slot section, the previous index, and the index of the equipment slot
    //NOTE: this does not handle error checking that will be done in _playerInventory
    public bool Equip(string prevSection, int prevIndex, int equipIndex)
    {   
        
        //instead of passing in the section string it searches the dictionary and sends in the actual section 
        return _playerInventory.Equip(_allSections[prevSection], prevIndex, equipIndex); 
    }

    //this unequips the item from the player inventory
    //it passes in the index of the previous equipment, a string representing the new section, and the index of the new section
    //NOTE: this does not handle error checking, that will be done in _playerInvenotry
    public bool Unequip(int prevIndex, string newSection, int newIndex)
    {   
        //instead of passin in the section string, it searches the dictionary and sends in the actual section 
        return _playerInventory.Unequip(prevIndex, _allSections[newSection], newIndex); 
    }
    //--------
    //getters
    //--------
    public Dictionary<Inventory, SlotHandler> GetCurrentTab() => _currentTab; 

    public string GetItemDescription(string section, int index)
    {   
        //get the item at that section and index
        ItemBase item = _allSections[section].GetItem(index); 

        //if it is null return "Select an Item!" so that shows up in the description panel
        if (item == null) return "Select an Item!"; 

        //otherwise get and return the item description of the item
        return item.description; 
        
    }

    public string GetItemFlavorText(string section, int index)
    {
        //get the item at that section and index
        ItemBase item = _allSections[section].GetItem(index); 

        //if it is null return an empty string so nothing shows up in that panel
        if (item == null) return ""; 

        //otherwise get and return the flavor text of the item
        return item.flavorText; 
    }
}