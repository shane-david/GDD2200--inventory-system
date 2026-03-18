using System.Collections.Generic;
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

    //dictionary representing the current tab, contains 
    //key of Inventories in that tab to their respective slot handlers
    private Dictionary<Inventory, SlotHandler> _currentTab = new(); 

    //dictionary from strings to all of the sections so that the tab handler
    //knows wich tab to call the SwapItems method on 
    private Dictionary<string, Inventory> _allSections = new(); 

    //----------------------
    //Unity Lifetime Methods
    //----------------------

    //build the dictionary for the gear tab as thats what the player will start on
    private void Awake()
    {   
        //instantiate player inventory
        _playerInventory = new PlayerInventory(); 

        //build the sections dictionary  
        _allSections.Add("weapons", _playerInventory.GetWeaponSection());
        _allSections.Add("clothing", _playerInventory.GetClothingSection()); 

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
    public void Equip(string prevSection, int prevIndex, int equipIndex)
    {   
        Debug.Log("Equipping from " + prevSection + " to equipment slot " + equipIndex); 
        //instead of passing in the section string it searches the dictionary 
        //and sends in the actual section 
        _playerInventory.Equip(); 
    }
    //--------
    //getters
    //--------
    public Dictionary<Inventory, SlotHandler> GetCurrentTab() => _currentTab; 
}