using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public ItemData itemData;

    public int Height
    {
        get
        {
            if(!isRotated)
            {
                return itemData.height;
            }
            return itemData.width;
        }
    }

    public int Width
    {
        get
        {
            if(!isRotated)
            {
                return itemData.width;
            }
            return itemData.height;
        }
    }

    [Header("ItemData")]
    public int onGridPositionX;
    public int onGridPositionY;
    public bool isRotated = false;

    internal void Set(ItemData itemData)
    {
        this.itemData = itemData;

        GetComponent<Image>().sprite = itemData.itemIcon;

        Vector2 size = new Vector2();
        size.x = itemData.width * ItemGrid.tileSizeWidth;
        size.y = itemData.height * ItemGrid.tileSizeHeight;

        GetComponent<RectTransform>().sizeDelta = size;
    }

    internal void Rotate()
    {
        isRotated = !isRotated;

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.rotation = Quaternion.Euler(0, 0, isRotated ? 90f : 0f);
    }
}
