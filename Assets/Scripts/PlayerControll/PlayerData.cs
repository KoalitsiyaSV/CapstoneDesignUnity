using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    //private static PlayerData _instance;

    public string playerName;// { get; set; }
    public float playerHP;// { get; set; }

    /*public static PlayerData Instance
    {
        get
        {
            if (_instance == null)
                _instance = new PlayerData();
            return _instance;
        }
    }*/
    
    /*public PlayerData()
    {
        playerName = "Test";
        playerHP = 200f;
    }*/
}