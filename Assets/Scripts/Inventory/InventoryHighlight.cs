using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHighlight : MonoBehaviour
{
    [SerializeField] RectTransform highlighter;
    [SerializeField] RectTransform RedHighlighter;

    public void Show(bool b)
    {
        highlighter.gameObject.SetActive(b);
    }

    public void SetSize(InventoryItem targetItem)
    {
        Vector2 size = new Vector2();
        size.x = targetItem.Width * ItemGrid.tileSizeWidth;
        size.y = targetItem.Height * ItemGrid.tileSizeHeight;
        highlighter.sizeDelta = size;
    }

    public void SetPosition(ItemGrid targetGrid, InventoryItem targetItem)
    {
        Vector2 pos = targetGrid.CalculatePositionOnGrid(
            targetItem,
            targetItem.onGridPositionX,
            targetItem.onGridPositionY
            );

        highlighter.localPosition = pos;
    }

    public void SetParant(ItemGrid targetGrid)
    {
        if(targetGrid == null) { return; }
        highlighter.SetParent(targetGrid.GetComponent<RectTransform>());
    }

    public void SetPosition(ItemGrid targetGrid, InventoryItem targetItem, int posX, int posY)
    {
        //�߰� ���: �������� ��ġ ���� ������ ���� highlighter�� ���� ����
        Vector2 pos = targetGrid.CalculatePositionOnGrid(
            targetItem,
            posX,
            posY
            );

        highlighter.localPosition = pos;
    }

    //public void SetColor(int red, int green, int blue)
    //{
    //    Image image = GetComponent<Image>();
    //    image.color = new Color(red, green, blue, 1f);
    //}
}
