using System.Collections.Generic;
using System.Data;
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
    [SerializeField] private SlotHandler _accessorySlots;
    [SerializeField] private SlotHandler _suppliesSlots;
    [SerializeField] private SlotHandler _foodSlots;


    //assigned in the insepctor, the parent object of all the tabs so that they can be turned on and off
    [Header("Drag and drop in each tab")]
    [SerializeField] private GameObject _gearTab;  
    [SerializeField] private GameObject _toolsTab;  
    [SerializeField] private GameObject _suppliesTab;
    [SerializeField] private GameObject _foodTab; 

    //assigned in the inspector, the button objects that represent the tabs
    [Header("Drag and drop in each button")]
    [SerializeField] private RectTransform _gearButton; 
    [SerializeField] private RectTransform _toolsButton; 
    [SerializeField] private RectTransform _suppliesButton; 
    [SerializeField] private RectTransform _foodButton; 


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
        _allSections.Add("Tools", _playerInventory.GetAccessorySection()); 
        _allSections.Add("Supplies", _playerInventory.GetSuppliesSection()); 
        _allSections.Add("Food", _playerInventory.GetFoodSection()); 

    }


    //---------------
    //public methods
    //---------------
    public void ChangeTab(string tabName)
    {   

        //clear all tabs
        _currentTab.Clear(); 

        //disable all tabs
        _gearTab.SetActive(false); 
        _toolsTab.SetActive(false); 
        _suppliesTab.SetActive(false); 
        _foodTab.SetActive(false); 

        //reset size of all tabs
        _gearButton.sizeDelta = new Vector2(65, 30);
        _toolsButton.sizeDelta = new Vector2(65, 30);
        _suppliesButton.sizeDelta = new Vector2(65,30); 
        _foodButton.sizeDelta = new Vector2(65,30); 

        switch (tabName)
        {   
            //weapons and clothing section within the gear tab 
            case "gear": 

                //add inventories for the gear tab
                _currentTab.Add(_playerInventory.GetWeaponSection(), _weaponSlots);
                _currentTab.Add(_playerInventory.GetClothingSection(), _clothingSlots);  

                //size up the gear tab button
                _gearButton.sizeDelta = new Vector2(90,40); 

                //enable the gear tab 
                _gearTab.SetActive(true); 
                break;

            case "tools":

                //add inventory for tools tabl
                _currentTab.Add(_playerInventory.GetAccessorySection(), _accessorySlots); 

                //size up the tools tab button 
                _toolsButton.sizeDelta = new Vector2(90,40);

                //enable the tools tab 
                _toolsTab.SetActive(true); 
                break; 

            case "supplies":

                //add inventory for supplies tab
                _currentTab.Add(_playerInventory.GetSuppliesSection(), _suppliesSlots); 

                //size up supplies tab button
                _suppliesButton.sizeDelta = new Vector2(90,40); 

                //enable the supplies tab
                _suppliesTab.SetActive(true);
                break; 

            case "food":

                //add inventory for food tab
                _currentTab.Add(_playerInventory.GetFoodSection(), _foodSlots); 

                //size up food tab button
                _foodButton.sizeDelta = new Vector2(90,40);

                //enable the food tab
                _foodTab.SetActive(true);
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

    //this checks if the item is a consumeable and it can be used, then it calles the use 
    //method in that IItemType instance, it returns whether the consumption was valid 
    public bool Consume(string section, int index)
    {
        //get the item and its ConsumeableType
        ItemBase item = _allSections[section].GetItem(index); 
        ConsumeableType consume = item.GetItemType() as ConsumeableType; 

        //if the consumeable type is valid add the item to the use context and call use 
        if (consume != null)
        {   
            _playerInventory.useCtx.item = item; 
            consume.Use(_playerInventory.useCtx); 
        }

        return false; 
    }

    public bool Stack(string section, int thisIndex, int otherIndex)
    {
        bool stackResult = false; 

        //get the items 
        ItemBase thisItem = _allSections[section].GetItem(thisIndex); 
        ItemBase otherItem = _allSections[section].GetItem(otherIndex); 

        //try to stack and store the result in a variable if both of the items are not null
        if (thisItem != null && otherItem != null) { 
            stackResult = thisItem.GetStackBehavior().TryToStack(thisItem, otherItem); 
        }

        //if the result of teh stack was true destroy this item because its quantity was given to other item
        if (stackResult)
        {
            _allSections[section].RemoveItem(thisIndex);  
        }

        //return the stack result so that the interaction handler knows what happend
        return stackResult; 

        
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