using UnityEngine;
using UnityEngine.EventSystems;

public class SlotInteractionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    //the slot outline so that it can activate and deactivate it 
    [SerializeField] private GameObject slotOutline; 

    //the tab handler so we can call swap items
    private TabHandler _tabHandler; 

    //slot needs to know its own index for switching
    public int SlotIndex;

    //the sectoin that the slot belongs to, this is so that items can not be placed 
    //in different sections and so that the slot can tell the tabhandler what section to change 
    [SerializeField] private string SlotSection;

    //----------------------
    //Unity Lifetime Methods
    //----------------------

    //at the beginning of the game get the tab handler component and make sure it exists 
    private void Awake()
    {
        _tabHandler = GetComponentInParent<TabHandler>(); 
        if (_tabHandler == null)
        {
            Debug.LogWarning("Invalid Tab Handler!");
            return; 
        }

        //set the slot section
        var slotGroup = GetComponentInParent<SlotHandler>(); 
        if (slotGroup != null)
        {
            SlotSection = slotGroup.SlotSection; 
        }
    }
    //-------------------
    //Raycasting handling
    //-------------------

    //when the raycast enters the slot enable the outline for user feedback
    //TODO display the flavor text and description in the ItemDescription panel
    public void OnPointerEnter(PointerEventData eventData)
    {
        slotOutline.SetActive(true); 
    }

    //when the raycast leaves the slot disable the outline for user feedback
    //TODO disable the flave text and description in the ItemDescription panel
    public void OnPointerExit(PointerEventData eventData)
    {
        slotOutline.SetActive(false); 
    }

    //TODO make sure there is an item in the slot 
    //TODO make a ghost copy for dragging feedback
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Original Index: " + SlotIndex); 
    }

    //TODO make the ghost copy follow the mouse 
    public void OnDrag(PointerEventData eventData)
    {
        
    }

    //when the player is done dragging the raycast needs to determine where the mouse is 
    //if it is over a valid slot it needs to initate a swap, if it is not it need to snap back
    public void OnEndDrag(PointerEventData eventData)
    {   

        //snap back boolean so that we know whether to do the snap back or not 
        bool snapBack = false; 

        //get the slot that the mouse in on when the dragging ends 
        SlotInteractionHandler targetSlot = eventData.pointerCurrentRaycast.gameObject?.GetComponent<SlotInteractionHandler>();

        //check if the next slot is equipment, if so equip, equipment handling and error checking will be handled in equip
        if (targetSlot?.SlotSection == "equipment")
        {   
            //set the opposite result of the equip to snapBack so that if it returns a false, we know we need to snap back
            snapBack = !_tabHandler.Equip(SlotSection, SlotIndex, targetSlot.SlotIndex); 
        }

        //next test if this slot is equipment, if so, and the target slot is valid, unequip 
        if (SlotSection == "equipment" && targetSlot != null)
        {
            //set the opposite reslt of the unequip to snapBack so that if it returns a false, we know we need to snap back 
            snapBack = !_tabHandler.Unequip(SlotIndex, targetSlot.SlotSection, targetSlot.SlotIndex); 
        }

        //if it is null do not swap 
        if (targetSlot == null || (targetSlot.SlotSection != this.SlotSection && targetSlot.SlotSection != "equipment") || snapBack)
        {
            Debug.Log("snap back");
            return;
        }

        //swap the items based off of the target slots index 
        _tabHandler.SwapItems(SlotIndex, targetSlot.SlotIndex, SlotSection); 
    }
}
