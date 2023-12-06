using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepMonster : EnemyController
{
    [SerializeField] bool isSleep;
    [SerializeField] float coolDown;

    protected override void Awake()
    {
        base.Awake();

        enemyAttackCooldown = coolDown;
    }

    protected override void FixedUpdate()
    {
        EnemyAwakeStart();

        base.FixedUpdate();
    }

    private void EnemyAwakeStart()
    {
        if(targetObj != null && isSleep)
        {
            isAction = true;
            isSleep = false;
            enemyAnimator.SetFloat("Speed", 1);
        }
    }

    private void EnemyAwakeEnd()
    {
        isAction = false;
    }

    protected override void EnemyDirectionChange()
    {
        if (transform.position.x > targetObj.position.x)
        {
            transform.localScale = new Vector2(-1, 1);
            enemyDirection = true;
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
            enemyDirection = false;
        }
    }
}