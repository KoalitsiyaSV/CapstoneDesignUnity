using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : EnemyController
{
    [SerializeField] float slimeJumpForce;
    [SerializeField] bool slimeDoJump;

    protected override void AfterPlayerDetect()
    {
        if (targetObj == null)
        {
            SightRange();
        }
        
        if (targetObj != null)
        {
            
            if (!isAction)
            {
                EnemyDirectionChange();
                SlimeJump();
            }
            else
            {
                enemyAnimator.SetBool("isJump", false);
            }

            if (slimeDoJump)
                MoveToPlayer();
        }
    }

    protected override void MoveToPlayer()
    {
        Debug.Log("Here");
        Vector2 targetPos = new Vector2(targetObj.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, enemy_Move_Speed * Time.deltaTime);
    }

    private void SlimeJump()
    {
        enemyAnimator.SetBool("isJump", true);
        enemyRigidbody.AddForce(Vector2.up * slimeJumpForce, ForceMode2D.Impulse);
        slimeDoJump = true;

        StartCoroutine(EnemyAttackCoolDown(4f));
    }

    private void SlimeJumpEnd()
    {
        slimeDoJump = false;
    }
}
