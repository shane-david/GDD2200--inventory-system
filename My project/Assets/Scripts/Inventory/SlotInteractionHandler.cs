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
    //TODO there needs to be some sort of way to tell the tab handler what sectoin to invoke the swap in or to do an equipmentswap
    public void OnEndDrag(PointerEventData eventData)
    {   

        //get the slot that the mouse in on when the dragging ends 
        SlotInteractionHandler targetSlot = eventData.pointerCurrentRaycast.gameObject?.GetComponent<SlotInteractionHandler>();

        //if it is null do not swap 
        if (targetSlot == null)
        {
            Debug.Log("snap back");
            return;
        }

        //swap the items based off of the target slots index 
        _tabHandler.SwapItems(SlotIndex, targetSlot.SlotIndex); 
    }
}
