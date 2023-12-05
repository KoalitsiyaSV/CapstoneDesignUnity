using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Consumable/Attack")]
public class ItemAttackPointIncrease : ItemEffect
{
    public int attackPoint = 0;

    public override bool ExcuteRole()
    {
        GameManager.Instance.PlayerAttackIncrease(attackPoint);

        return true;
    }
}