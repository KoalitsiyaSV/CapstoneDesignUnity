using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipment, Consumable, Boost, ETC
}

[System.Serializable]
public class Item
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;
    public List<ItemEffect> itemEffects;

    public bool use()
    {
        bool isUsed = false;
        
        foreach(ItemEffect itemEffect in itemEffects)
        {
            isUsed = itemEffect.ExcuteRole();
        }

        return isUsed;
    }
}
