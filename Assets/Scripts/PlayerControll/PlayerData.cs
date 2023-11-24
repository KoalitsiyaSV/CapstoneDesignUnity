using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    private static PlayerData _instance;

    public string playerName { get; private set; }
    public float playerHP { get; private set; }

    public static PlayerData Instance
    {
        get
        {
            if (_instance == null)
                _instance = new PlayerData();
            return _instance;
        }
    }

    private PlayerData()
    {
        playerName = "Test";
        playerHP = 200f;
    }
}