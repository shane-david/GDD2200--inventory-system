using TMPro;
using UnityEngine; 
using UnityEngine.UI; 


public class SlotHandler : MonoBehaviour
{
    
    //variable set in the inspector representing the type of items
    //that are stored in this group of slots
    public string SlotSection; 

    public void BuildSlots(Inventory inventoryData)
    {   
        //get the list of items from the inventory 
        var inventoryItems = inventoryData.GetAllItems(); 

        //iterate through the total slots in the inventory
        for (int i = 0; i < inventoryItems.Count; i++)
        {      
            
            //get the image component of the respective slot in the SlotHandler
            Image itemImage = transform.GetChild(i).Find("ItemImage").GetComponent<Image>(); 
            TextMeshProUGUI amountText = transform.GetChild(i).Find("AmountText").GetComponent<TextMeshProUGUI>(); 

            //if the item is null, there is nothing in that slot, make sure the sprite is nulle and alpha is 0 
            if (inventoryItems[i] == null)
            {
                itemImage.sprite = null;
                itemImage.color = new Color(1f,1f,1f,0f); 
                amountText.text = ""; 

            //otherwise there is an item, so load the sprite and put it in the itemImage 
            } else {

                //load the sprite using the id of the item in the inventory 
                Sprite loaded = Resources.Load<Sprite>("Sprites/" + inventoryItems[i].id); 

                //set the sprite of the slot to the sprite we just loaded in and 
                //change the alpha so that it is visible 
                itemImage.sprite = loaded; 

                //only refresh the alpha if it is not currently being dragged 
                if (SlotInteractionHandler.GetDragGhost() == null) {
                    itemImage.color = new Color(1f, 1f, 1f, 1f); 
                }
                
                //if the amount of the item is 1 do not display the amount text
                if (inventoryItems[i].quantity == 1) {

                    amountText.text = ""; 
                
                //if the amount of the item is 0 destroy it 
                } else if (inventoryItems[i].quantity == 0) {

                    inventoryData.RemoveItem(i); 
                    
                //otherwise set the amount text to its proper value 
                } else {
                    amountText.text = "[" + inventoryItems[i].quantity + "]"; 
                }      
            }

        }
    }
}