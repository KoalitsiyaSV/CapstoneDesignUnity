using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Consumable/MovementSpeed")]
public class ItemMovementSpeedIncrease : ItemEffect
{
    public float movementSpeedScale = 0;

    public override bool ExcuteRole()
    {
        GameManager.Instance.PlayerMovementSpeedIncrease(movementSpeedScale);

        return true;
    }
}
