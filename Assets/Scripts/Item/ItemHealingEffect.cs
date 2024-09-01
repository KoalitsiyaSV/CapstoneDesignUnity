using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Consumable/Health")]
public class ItemHealingEffect : ItemEffect
{
    public int healingPoint = 0;
    public override bool ExcuteRole()
    {
        GameManager.Instance.PlayerHealed(healingPoint);

        return true;
    }
}
