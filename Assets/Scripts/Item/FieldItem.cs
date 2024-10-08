using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItem : MonoBehaviour
{
    public Item item;
    public SpriteRenderer itemSprite;
    public bool canPickUp = false;
    public CircleCollider2D pickupRange;

    private void Start()
    {
        itemSprite = GetComponent<SpriteRenderer>();
        pickupRange = GetComponent<CircleCollider2D>();

        Invoke("ReversePickUpFlag", 2f);
    }

    public void SetItem(Item _item)
    {
        item.itemType = _item.itemType;
        item.itemName = _item.itemName;
        item.itemImage = _item.itemImage;
        item.itemEffects = _item.itemEffects;

        if(itemSprite != null)
            itemSprite.sprite = item.itemImage;
    }

    public Item GetItem()
    {
        return item;
    }

    private void ReversePickUpFlag()
    {
        pickupRange.enabled = true;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}