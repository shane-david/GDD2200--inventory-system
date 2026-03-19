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
            new EquipmentType("weapon", "attack", 10, 1)
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
            new EquipmentType("boot", "jumpPower", 10, 4)
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
            new EquipmentType("backpack", "weight", 20, 0)
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
            new EquipmentType("hat", "catchRate", 10, 2)
        ); 

        return fishingHat; 
    }

    public ItemBase CreateCowboyHat()
    {
        ItemBase cowboyHat = new(
            "Cowboy Hat",
            "Found on the road up to the abonded town, likely belonged to a forgotten hero.",
            "Increase your health!",
            "cowboyHat",
            1,
            new EquipmentType("hat", "health", 10, 2)
        );

        return cowboyHat; 
    }

    public ItemBase CreateRope()
    {
        ItemBase rope = new(
            "Rope",
            "Thrown in your pack last minute. Not very long, not very strong.",
            "Might come in useful further up the mountain...",
            "rope",
            1,
            new EquipmentType("accessory", "climbPower", 10, 5)
        );

        return rope; 
    }

    public ItemBase CreateFishingRod()
    {
        ItemBase fishingRod = new(
            "Fishing Rod",
            "Packed in your bag by your Dad hoping you catch some fish on your trip",
            "Apparently theres a lake in the middle of the mountain...",
            "basicRod", 
            1,
            new EquipmentType("accessory", "catchRate", 20, 5)
        ); 

        return fishingRod; 
    }

    public ItemBase CreateFlashlight()
    {
        ItemBase flashlight = new(
            "Flash Light",
            "First thing you packed, everyone knows you need a flashlight!",
            "Dont forget to replace the batteries soon...",
            "flashlight",
            1,
            new EquipmentType("accessory", "vision", 50, 3)
        ); 

        return flashlight; 
    }
}