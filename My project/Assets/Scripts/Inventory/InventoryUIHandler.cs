using TMPro;
using UnityEngine;

public class InventoryUIHandler : MonoBehaviour
{

    //put in the tab handler so we can get slots and refresh the inventory
    private TabHandler _tabHandler; 

    //get the text mesh pro objects that need to be changed real tiem
    [SerializeField] private TMP_Text _descriptionTextTMP; 
    [SerializeField] private TMP_Text _flavorTextTMP; 

    //----------------------
    //Unity lifetime methods
    //----------------------

    //when the inventory starts
    private void Start()
    {
        _tabHandler = GetComponent<TabHandler>(); 
        if (_tabHandler == null)
        {
            Debug.LogWarning("Needs Tab Hanlder Component!"); 
        }

        _tabHandler.ChangeTab("gear"); 
        RefreshInventory(); 
        RefreshDescriptionPanel(); 
    }

    private void Update()
    {
        RefreshInventory(); 
    }

    //this looks at the current tab of the playerInventory and 
    //builds all of the slots based off if the items in those two Inventory instances
    private void RefreshInventory()
    {
        foreach (var section in _tabHandler.GetCurrentTab())
        {    
            section.Value.BuildSlots(section.Key); 
        }
    }

    //this recieves an item index and section and tells the tab handler
    //to get the description and flavor text of that item, it then sets 
    //the TMP elements in the inventory UI to those values 
    public void RefreshDescriptionPanel(string section, int index)
    {   
        _descriptionTextTMP.color = Color.black; 
        _descriptionTextTMP.text = _tabHandler.GetItemDescription(section, index);
        _flavorTextTMP.text = _tabHandler.GetItemFlavorText(section, index); 
    }

    //overriden method that recieves no arguments, this one will set the description text
    //and flavor text to empty strings
    public void RefreshDescriptionPanel()
    {   
        _descriptionTextTMP.color = Color.black; 
        _descriptionTextTMP.text = "";
        _flavorTextTMP.text = ""; 
    }

    //sets the desctiptoin panel to say that the item can not go there when an item is dragged somewhere it can not go
    public void SnapBack()
    {
        _descriptionTextTMP.text = "Item can not go there!";
        _descriptionTextTMP.color = Color.red; 
    }
}
