using UnityEngine;
using UnityEngine.EventSystems;

public class SlotInteractionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private GameObject slotOutline; 
    
    //-------------------
    //Raycasting handling
    //-------------------

    public void OnPointerEnter(PointerEventData eventData)
    {
        slotOutline.SetActive(true); 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        slotOutline.SetActive(false); 
    }
}
