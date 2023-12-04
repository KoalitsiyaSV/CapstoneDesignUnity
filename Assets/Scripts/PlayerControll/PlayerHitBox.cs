using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : HitBox
{
    protected override void Awake()
    {
        base.Awake();

        targetLayerName = "Enemy";
    }

    protected override void OnHit(Collider2D collision)
    {
        EnemyController enemyCollider = collision.gameObject.GetComponent<EnemyController>();
        enemyCollider.OnDamaged(10);
    }
}