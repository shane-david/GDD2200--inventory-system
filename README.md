## Steps
* Run the Unity Project
* You will be automatically tabbed into the gear tab showing weapons and clothing.
* Hover over an item to view its Name, Description, and Flavor Text in the bottom right panel 
* Left click on an item to begin dragging it
* Drag into a proper slot and it will go to that slot 
* Drag into an inproper slot and it will return to its previous slot and display an error in the bottom right panel
* Note that equipping an item updates the stat bars in the bottom left 
* Note that what equipment piece goes in what slot is denoted by the semi-transparent icons in the equipment slots and matching icons above grid categories
* Left click the tabs on the top to toggle between different inventory categories 
* In supplies tab drag two items of the same type on top of eachother to stack them
* In food tab right click to consume food. Note health stat bar being updated live when food is consumed

## Controls
* Mouse Controls
* Hover over items to populate description panel
* Left click and hold to begin dragging 
* Right click consuemable items to consume

## Credits
* GDD2200 Developing a Mini-Inventory Slides
* Youtube video used for creating stat bars: https://www.youtube.com/watch?v=0tDPxNB2JNs
* Unity Documentation for IPointer interfaces in SlotInteractoinHandler

## Known Issues
* When dragging and item to a new slot it does not populate the text panel unless the mouse leaves and reenters the slot
* No feedback for trying to consume a nonconsumeable item. 
* Error message for dragging to an inproper slot only remains until the mouse does something (enter a slot, leave a slot) to trigger another IPointer method. 
* Inventory Capacity is hardcoded and therefore static so there is no way to change how much an inventory can hold depending on the equipped backpack. 
* Code that was written later in the project was done so with the intent of getting it working and could have much better OOP design to limit conditionals and references being passed between multiple objects. 
* Many passed in strings representing things like grid names, tab names, ect. could be replaced with enums for better code and avoidance of null reference errors as a result of passing in the incorrect string. 