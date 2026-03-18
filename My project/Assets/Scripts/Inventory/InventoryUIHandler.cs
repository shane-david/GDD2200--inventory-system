using UnityEngine;

public class InventoryUIHandler : MonoBehaviour
{

    //put in the tab handler so we can get slots and refresh the inventory
    private TabHandler _tabHandler; 

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
}
