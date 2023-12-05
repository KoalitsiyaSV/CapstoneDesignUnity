using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Consumable/HealthPoint")]
public class ItemHealthPointIncrease : ItemEffect
{
    public int healthPoint = 0;

    public override bool ExcuteRole()
    {
        GameManager.Instance.PlayerHealthIncrease(healthPoint);

        return true;
    }
}
