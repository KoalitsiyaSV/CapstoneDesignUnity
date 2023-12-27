using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinDagger : EnemyController
{
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        enemyPatternCount = 1;
        enemyAttackCooldown = 2.5f;
    }
    private void Update()
    {
        enemySightStartPoint = enemyRigidbody.position.x + 4f;
    }
}
