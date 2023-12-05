using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;

    public GameObject fieldItemPrefab;
    public Vector2[] Pos;

    private void Start()
    {
            
    }

    private void Awake()
    {
        instance = this;
    }

    public List<Item> itemDB = new List<Item>();
}
