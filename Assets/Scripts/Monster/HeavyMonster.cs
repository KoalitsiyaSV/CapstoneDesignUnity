using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyMonster : EnemyController
{
    protected override void Awake()
    {
        base.Awake();

        enemyPatternCount = 2;
        enemyAttackCooldown = 4f;
    }

    private void Update()
    {
        enemySightStartPoint = enemyRigidbody.position.x + 4f;
    }

    ////아마 아래를 부모에 적용시켜도 문제 없을거라 예상함
    //protected override void StartAttackAnim()
    //{
    //    int rnd = Random.Range(1, enemyPatternCount + 1);

    //    enemyAnimator.SetInteger("EnemyAttack", rnd);

    //    StartCoroutine(EnemyAttackCoolDown(4f));
    //}

    //protected override void EndAttackAnim()
    //{
    //    enemyAnimator.SetInteger("EnemyAttack", 0);
    //}
}