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
    public Canvas canvas;

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

        gridPosition.x = (int)(positionOnTheGrid.x / (tileSizeWidth * canvas.scaleFactor));
        gridPosition.y = -(int)(positionOnTheGrid.y / (tileSizeHeight * canvas.scaleFactor));

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

    // ? �߰�
    public Vector2Int? FindSpaceForObject(InventoryItem itemToInsert)
    {
        int width = gridSizeWidth - itemToInsert.Width + 1;
        int height = gridSizeHeight - itemToInsert.Height + 1;
        

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if(CheckAvailableSpace(x, y, itemToInsert.Width, itemToInsert.Height) == true)
                { 
                    return new Vector2Int(x,y);
                }
            }
        }

        return null;
    }

    private void CleanGridReference(InventoryItem toReturn)
    {
        for (int ix = 0; ix < toReturn.Width; ix++)
        {
            for (int iy = 0; iy < toReturn.Height; iy++)
            {
                inventoryItemSlot[toReturn.onGridPositionX + ix, toReturn.onGridPositionY + iy] = null;
            }
        }
    }

    // [posX, posY]�� ������ �α�
    public bool PlaceItem(InventoryItem inventoryItem, int posX, int posY, ref InventoryItem overlapItem)
    {
        if (!BoundaryCheck(posX, posY, inventoryItem.Width, inventoryItem.Height))
        {
            return false;
        }

        if (!OverlapCheck(posX, posY, inventoryItem.Width, inventoryItem.Height, ref overlapItem))
        {
            overlapItem = null;
            return false;
        }

        if (overlapItem != null)
        {
            CleanGridReference(overlapItem);
        }

        PlaceItem(inventoryItem, posX, posY);

        return true;
    }

    public void PlaceItem(InventoryItem inventoryItem, int posX, int posY)
    {
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform);

        for (int x = 0; x < inventoryItem.Width; x++)
        {
            for (int y = 0; y < inventoryItem.Height; y++)
            {
                inventoryItemSlot[posX + x, posY + y] = inventoryItem;
            }
        }

        inventoryItem.onGridPositionX = posX;
        inventoryItem.onGridPositionY = posY;
        Vector2 position = CalculatePositionOnGrid(inventoryItem, posX, posY);

        rectTransform.localPosition = position;
    }

    public Vector2 CalculatePositionOnGrid(InventoryItem inventoryItem, int posX, int posY)
    {
        Vector2 position = new Vector2();
        position.x = posX * tileSizeWidth + tileSizeWidth * inventoryItem.Width / 2;
        position.y = posY * tileSizeHeight + tileSizeHeight * inventoryItem.Height / 2;
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
    private bool CheckAvailableSpace(int posX, int posY, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (inventoryItemSlot[posX + x, posY + y] != null)
                {
                    return false;
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
