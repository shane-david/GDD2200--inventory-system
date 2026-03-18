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

            //if the item is null, there is nothing in that slot, make sure the sprite is nulle and alpha is 0 
            if (inventoryItems[i] == null)
            {
                itemImage.sprite = null;
                itemImage.color = new Color(1f,1f,1f,0f); 

            //otherwise there is an item, so load the sprite and put it in the itemImage 
            } else {

                //load the sprite using the id of the item in the inventory 
                Sprite loaded = Resources.Load<Sprite>("Sprites/" + inventoryItems[i].id); 

                //set the sprite of the slot to the sprite we just loaded in and 
                //change the alpha so that it is visible 
                itemImage.sprite = loaded; 
                itemImage.color = new Color(1f, 1f, 1f, 1f); 
                
            }
        }
    }
}