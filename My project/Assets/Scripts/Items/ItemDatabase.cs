public class ItemDatabase
{
    //-----------------------
    //methods to create items
    //-----------------------

    public ItemBase CreatePocketKnife()
    {
        ItemBase pocketKnife = new(
            "Pocket Knife", 
            "Simple surival tool packed in your bag when you began the adventure",
            "Looks like it needs to be sharpened",
            "pocketKnife",
            1,
            new EquipmentType("weapon", "attack", 10)
        ); 

        return pocketKnife; 
    }

    public ItemBase CreateBoots()
    {
        ItemBase boots = new(
            "Jump Boots",
            "Boots found in the depths of the Leafspring Gardens, stolen by the shop keeper",
            "These boot give you mystical abilities!",
            "boots",
            1,
            new EquipmentType("boot", "jumpPower", 10)
        ); 

        return boots; 
    }

    public ItemBase CreateLightBackpack()
    {
        ItemBase lightBackpack = new(
            "Light Backpack",
            "This pack feels light and is easy to move in but it can't hold that much stuff",
            "Maybe only use it for short side adventures!",
            "backpack",
            1,
            new EquipmentType("backpack", "weight", 20)
        ); 

        return lightBackpack; 
    }

    public ItemBase CreateFishingHat()
    {
        ItemBase fishingHat = new(
            "Fishing Hat",
            "Packed in your bag by your Dad hoping you catch some fish on your trip.",
            "Increases your catch rate!",
            "hat",
            1,
            new EquipmentType("hat", "catchRate", 10)
        ); 

        return fishingHat; 
    }
}