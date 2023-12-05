using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : HitBox
{
    protected override void Awake()
    {
        base.Awake();

        targetLayerName = "Player";
    }

    protected override void OnHit(Collider2D collision)
    {
        GameManager.Instance.PlayerTakeDamage(10);
    }
}