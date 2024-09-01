using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Consumable/Armor")]
public class ItemArmorPointIncrease : ItemEffect
{
    public int armorPoint = 0;

    public override bool ExcuteRole()
    {
        GameManager.Instance.PlayerArmorIncrease(armorPoint);

        return true;
    }
}
