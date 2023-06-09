using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryController : MonoBehaviour
{
    [HideInInspector]// �ν����� â���� �����
    public ItemGrid selectedItemGrid;

    InventoryItem selectedItem;
    InventoryItem overlapItem;
    RectTransform rectTransform;

    [SerializeField] List<ItemData> items;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform canvasTransform;

    InventoryHighlight inventoryHighlight;

    private void Awake()
    {
        inventoryHighlight = GetComponent<InventoryHighlight>();
    }

    private void Update()
    {
        ItemIconDrag();

        

        if(Input.GetKeyUp(KeyCode.Q))
        {
            CreateRandomItem();
        }

        if (selectedItemGrid == null) 
        {
            inventoryHighlight.Show(false);
            return; 
        }

        HandleHighlight();

        //��Ŭ�� �� �̺�Ʈ �߻�
        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseButtonPress();
        }
    }

    InventoryItem itemToHighlight;

    private void HandleHighlight()
    {
        Vector2Int positionOnGrid = GetTileGridPosition();

        if (selectedItem == null)
        {
            itemToHighlight = selectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y);

            if (itemToHighlight != null)
            {
                inventoryHighlight.Show(true);
                inventoryHighlight.SetSize(itemToHighlight);
                inventoryHighlight.SetParant(selectedItemGrid);
                inventoryHighlight.SetPosition(selectedItemGrid, itemToHighlight);
            }
            else
            {
                inventoryHighlight.Show(false);
            }
        }
        else
        {
            inventoryHighlight.Show(selectedItemGrid.BoundaryCheck(
                positionOnGrid.x,
                positionOnGrid.y,
                selectedItem.itemData.width,
                selectedItem.itemData.height
                ));
            inventoryHighlight.SetSize(selectedItem);
            inventoryHighlight.SetParant(selectedItemGrid);
            inventoryHighlight.SetPosition(selectedItemGrid, selectedItem, positionOnGrid.x, positionOnGrid.y);
        }
    }

    private void CreateRandomItem()
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        selectedItem = inventoryItem;
        
        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);

        int selectedItemID = UnityEngine.Random.Range(0, items.Count);
        inventoryItem.Set(items[selectedItemID]);
    }

    private void LeftMouseButtonPress()
    {
        Vector2Int tileGridPosition = GetTileGridPosition();

        //���õ� �������� ���ٸ�
        if (selectedItem == null)
        {
            //Ŭ���� ��ǥ�� �ִ� ������ ����
            PickUpItem(tileGridPosition);
        }
        //���õ� �������� �ִٸ�
        else
        {
            //Ŭ���� ��ǥ�� ������ ����
            PlaceItem(tileGridPosition);
        }
    }

    private Vector2Int GetTileGridPosition()
    {
        Vector2 position = Input.mousePosition;

        if (selectedItem != null)
        {
            position.x -= (selectedItem.itemData.width - 1) * ItemGrid.tileSizeWidth / 2;
            position.y -= (selectedItem.itemData.height - 1) * ItemGrid.tileSizeHeight / 2;
        }

        //��Ŭ�� �� ����Ʈ�� �׸��� ���� ��ǥ �ޱ�
        return selectedItemGrid.GetTileGridPosition(position);
    }

    private void PlaceItem(Vector2Int tileGridPosition)
    {
        Debug.Log("Item Placed");
        bool complete = selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y, ref overlapItem);
        
        if(complete)
        { 
            selectedItem = null; 
            if(overlapItem != null)
            {
                selectedItem = overlapItem;
                overlapItem = null;
                rectTransform = selectedItem.GetComponent<RectTransform>();
            }
        }
    }

    private void PickUpItem(Vector2Int tileGridPosition)
    {
        selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);

        if (selectedItem != null)
        {
            Debug.Log("Item Pickuped");
            rectTransform = selectedItem.GetComponent<RectTransform>();
        }
    }

    private void ItemIconDrag()
    {
        if (selectedItem != null)
        {
            rectTransform.position = Input.mousePosition;
        }
    }
}
