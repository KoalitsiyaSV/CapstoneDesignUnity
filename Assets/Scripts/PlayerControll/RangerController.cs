using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerController : PlayerController
{
    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0) && !playerAnimator.GetBool("isAttack"))
            StartAttackAnim();

        if (Input.GetMouseButton(1) && !playerAnimator.GetBool("isSkill"))
            StartSkillAnim();
    }

    protected override void StartSkillAnim()
    {
        base.StartSkillAnim();

        playerRigidbody.gravityScale = 0;
    }

    protected override void EndSkillAnim()
    {
        base.EndSkillAnim();

        playerRigidbody.gravityScale = 4;
    }
}
