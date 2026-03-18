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

    //----------------------
    //Unity Lifetime Methods
    //----------------------

    //build the dictionary for the gear tab as thats what the player will start on
    private void Start()
    {
        _playerInventory = new PlayerInventory(); 
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

    //--------
    //getters
    //--------
    public Dictionary<Inventory, SlotHandler> GetCurrentTab() => _currentTab; 
}