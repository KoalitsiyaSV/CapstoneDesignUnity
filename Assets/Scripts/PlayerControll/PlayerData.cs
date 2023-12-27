using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    private static PlayerData _instance;

    public float playerHealthPoint { get; private set; }
    public int playerAttackPoint { get; private set; }
    public float playerArmorPoint { get; private set; }
    public float playerMovementSpeedScale { get; private set; }

    //public float playerHP { get; private set; }

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
        playerHealthPoint = 1000f;
        playerAttackPoint = 30;
        playerArmorPoint = 0;
        playerMovementSpeedScale = 1f;
    }
}