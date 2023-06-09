using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    // �׸��� ������
    public const float tileSizeWidth = 32;
    public const float tileSizeHeight = 32;

    InventoryItem[,] inventoryItemSlot;

    RectTransform rectTransform;

    // �׸��� ����
    [SerializeField] int gridSizeWidth = 10;
    [SerializeField] int gridSizeHeight = 10;


    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Init(gridSizeWidth, gridSizeHeight);
    }

    // �׸��� ������ �ʱ�ȭ
    // �Էµ� ����ŭ �κ��丮 ���� �迭 ���� �� �׸��� ����
    private void Init(int width, int height)
    {
        inventoryItemSlot = new InventoryItem[width, height];
        Vector2 size = new Vector2(width * tileSizeWidth, height * tileSizeHeight);
        rectTransform.sizeDelta = size;
    }

    Vector2 positionOnTheGrid = new Vector2();
    Vector2Int gridPosition = new Vector2Int();

    // ���콺 �����Ͱ� ��ġ�� �׸��� ��ǥ ���
    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {
        positionOnTheGrid.x = mousePosition.x - rectTransform.position.x;
        positionOnTheGrid.y = rectTransform.position.y - mousePosition.y;

        gridPosition.x = (int)(positionOnTheGrid.x / tileSizeWidth);
        gridPosition.y = -(int)(positionOnTheGrid.y / tileSizeHeight);

        return gridPosition;
    }

    // [posX, posY]�� �ִ� ������ ����
    public InventoryItem PickUpItem(int x, int y)
    {
        InventoryItem toReturn = inventoryItemSlot[x, y];

        if (toReturn == null) { return null; }

        CleanGridReference(toReturn);

        return toReturn;
    }

    private void CleanGridReference(InventoryItem toReturn)
    {
        for (int ix = 0; ix < toReturn.itemData.width; ix++)
        {
            for (int iy = 0; iy < toReturn.itemData.height; iy++)
            {
                inventoryItemSlot[toReturn.onGridPositionX + ix, toReturn.onGridPositionY + iy] = null;
            }
        }
    }

    // [posX, posY]�� ������ �α�
    public bool PlaceItem(InventoryItem inventoryItem, int posX, int posY, ref InventoryItem overlapItem)
    {
        if (!BoundaryCheck(posX, posY, inventoryItem.itemData.width, inventoryItem.itemData.height))
        {
            return false;
        }

        if (!OverlapCheck(posX, posY, inventoryItem.itemData.width, inventoryItem.itemData.height, ref overlapItem))
        {
            overlapItem = null;
            return false;
        }

        if (overlapItem != null)
        {
            CleanGridReference(overlapItem);
        }

        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform);

        for (int x = 0; x < inventoryItem.itemData.width; x++)
        {
            for (int y = 0; y < inventoryItem.itemData.height; y++)
            {
                inventoryItemSlot[posX + x, posY + y] = inventoryItem;
            }
        }

        inventoryItem.onGridPositionX = posX;
        inventoryItem.onGridPositionY = posY;
        Vector2 position = CalculatePositionOnGrid(inventoryItem, posX, posY);

        rectTransform.localPosition = position;

        return true;
    }

    public Vector2 CalculatePositionOnGrid(InventoryItem inventoryItem, int posX, int posY)
    {
        Vector2 position = new Vector2();
        position.x = posX * tileSizeWidth + tileSizeWidth * inventoryItem.itemData.width / 2;
        position.y = posY * tileSizeHeight + tileSizeHeight * inventoryItem.itemData.height / 2;
        return position;
    }

    private bool OverlapCheck(int posX, int posY, int width, int height, ref InventoryItem overlapItem)
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                if (inventoryItemSlot[posX + x, posY + y] != null)
                {
                    if (overlapItem == null)
                    {
                        overlapItem = inventoryItemSlot[posX + x, posY + y];
                    }
                    else
                    {
                        if(overlapItem != inventoryItemSlot[posX + x, posY + y])
                        {
                            return false;
                        }
                    }
                }
            }
        }

        return true;
    }

    public bool PositionCheck(int posX, int posY)
    {
        if (posX < 0 || posY < 0) return false;
        if (posX >= gridSizeWidth || posY >= gridSizeHeight) return false;

        return true;
    }

    public bool BoundaryCheck(int posX, int posY, int width, int height)
    {
        if (!PositionCheck(posX,posY)) { return false; }

        posX += width - 1;
        posY += height - 1;

        if(!PositionCheck(posX, posY)) { return false; }

        return true;
    }

    internal InventoryItem GetItem(int x, int y)
    {
        return inventoryItemSlot[x, y];
    }
}
