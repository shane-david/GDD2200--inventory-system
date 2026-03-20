using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public class SlotInteractionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{

    //the slot outline so that it can activate and deactivate it 
    [SerializeField] private GameObject slotOutline; 

    //the slot icon so that we can create a ghost copy of it for the drag and drop effect
    [SerializeField] private Image itemIcon; 

    //the drag ghost game object that is static so that is is shared across all slots sicne only one thing can be dragged at a time 
    private static GameObject _dragGhost; 

    //the tab handler so we can call swap items
    private TabHandler _tabHandler; 

    //the inventory UI handler so we can access displaying text method
    private InventoryUIHandler _UIHandler; 

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

        _UIHandler = GetComponentInParent<InventoryUIHandler>();
        if (_UIHandler == null)
        {
            Debug.LogWarning("Invalid UI Hanlder!");
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
    public void OnPointerEnter(PointerEventData eventData)
    {   

        //get the slot that the mouse in on when hoevering over it  
        SlotInteractionHandler targetSlot = eventData.pointerCurrentRaycast.gameObject?.GetComponent<SlotInteractionHandler>();

        //tell the inventory UI handler to display the text 
        _UIHandler.RefreshDescriptionPanel(targetSlot.SlotSection, targetSlot.SlotIndex); 

        //outline 
        slotOutline.SetActive(true); 

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        //if it was a right click 
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            
            //get the slot information 
            SlotInteractionHandler targetSlot = eventData.pointerCurrentRaycast.gameObject?.GetComponent<SlotInteractionHandler>();

            //if it is not null, tell the tab handler to find the item and use it, error checking in tab handler
            if (targetSlot != null)
            {
                _tabHandler.Consume(targetSlot.SlotSection, targetSlot.SlotIndex); 
            }
        }
    }

    //when the raycast leaves the slot disable the outline for user feedback
    public void OnPointerExit(PointerEventData eventData)
    {   
        //tell the inventory UI handler to update to no text
        _UIHandler.RefreshDescriptionPanel(); 

        //unoutline 
        slotOutline.SetActive(false); 
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
       
        //make sure it does not try to create a ghost object of an empty slot 
        if (itemIcon == null || !itemIcon.enabled || itemIcon.sprite == null)
        {   
            //cancel the drag 
            eventData.pointerDrag = null;
            return; 
        }

        //make the current image invisible 
        itemIcon.color = new Color(itemIcon.color.r, itemIcon.color.g, itemIcon.color.b, 0f);

        //if it has not yet returned create the ghost image 

        //get the root canvas so that the ghost image can still exist on it when instantiated
        Canvas inventoryCanvas = GetComponentInParent<Canvas>().rootCanvas; 

        //create the drag ghost, set its parent, make sure it is rendered on top 
        _dragGhost = new GameObject("DragGhost"); 
        _dragGhost.transform.SetParent(inventoryCanvas.transform, false); 
        _dragGhost.transform.SetAsLastSibling(); 

        //set the drag ghost's sprite and make sure it does not block raycast targets
        Image ghostImage =_dragGhost.AddComponent<Image>();
        ghostImage.sprite = itemIcon.sprite; 
        ghostImage.raycastTarget = false; 

        //set the size of the ghost image to be 1.5x bigger than the orignial icon to indicate dragging 
        _dragGhost.GetComponent<RectTransform>().sizeDelta = itemIcon.GetComponent<RectTransform>().rect.size * 1.5f; 

        //now make the ghost's position be the same as the moust position 
        _dragGhost.transform.position = eventData.position;  

    }

    public void OnDrag(PointerEventData eventData)
    {   
        //if there is a valid drag ghost, drag it 
        if (_dragGhost != null)
        {
            _dragGhost.transform.position = eventData.position; 
        }
    }


    //when the player is done dragging the raycast needs to determine where the mouse is 
    //if it is over a valid slot it needs to initate a swap, if it is not it need to snap back
    public void OnEndDrag(PointerEventData eventData)
    {   
        
        //first need to clean up the ghost icon by destroying the ghost game object
        if (_dragGhost != null)
        {
            Destroy(_dragGhost);
            _dragGhost = null; 
        }

        //next reset the alpah of the item icon
        itemIcon.color = new Color(itemIcon.color.r, itemIcon.color.g, itemIcon.color.b, 1f); 

        //snap back boolean so that we know whether to do the snap back or not 
        bool snapBack = false; 

        //get the slot that the mouse in on when the dragging ends 
        SlotInteractionHandler targetSlot = eventData.pointerCurrentRaycast.gameObject?.GetComponent<SlotInteractionHandler>();

        //check if the next slot is equipment, if so equip, equipment handling and error checking will be handled in equip
        if (targetSlot?.SlotSection == "equipment") { 

            //set the opposite result of the equip to snapBack so that if it returns a false, we know we need to snap back
            snapBack = !_tabHandler.Equip(SlotSection, SlotIndex, targetSlot.SlotIndex); 

        //next test if this slot is equipment, if so, and the target slot is valid, unequip 
        } else if (targetSlot != null && SlotSection == "equipment") {

            //set the opposite reslt of the unequip to snapBack so that if it returns a false, we know we need to snap back 
            snapBack = !_tabHandler.Unequip(SlotIndex, targetSlot.SlotSection, targetSlot.SlotIndex); 

        //otherwise it is not an equip or unequip so check if is going in the right section and set snapback
        } else if (targetSlot == null || this.SlotSection != targetSlot.SlotSection) {

            snapBack = true; 

        //otherwise there has been a proper swap so swap the items
        } else {   

            //try to stack 
            bool didStack = _tabHandler.Stack(SlotSection, SlotIndex, targetSlot.SlotIndex); 

            //if the stack did not go through swap the items based off of the target slots index 
            if (!didStack) {
                _tabHandler.SwapItems(SlotIndex, targetSlot.SlotIndex, SlotSection); 
            }
        }

        //if it is null do not swap 
        if (targetSlot == null ||  snapBack)
        {
            _UIHandler.SnapBack(); 
            return;
        }

    }

    //getter the drag ghost so the slot handler knows whether to render the image or not 
    public static GameObject GetDragGhost() => _dragGhost; 
}
