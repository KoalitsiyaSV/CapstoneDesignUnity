using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public Transform Player;
    public GameObject[] dungeonObject;
    private Vector2 spawnPointPos;

    private void Start()
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ChangeMap();
        }
    }

    private void ChangeMap()
    {
        Player.position = new Vector2(2.5f, 3f);
        dungeonObject[0].SetActive(false);
        dungeonObject[1].SetActive(true);
    }
}