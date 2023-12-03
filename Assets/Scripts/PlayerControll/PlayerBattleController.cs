using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBattleController : MonoBehaviour
{
    private Animator playerAnimator;
    private SpriteRenderer playerSpriteRenderer;
    [SerializeField] Collider2D[] colliderComponents;

    [Header("PlayerAttack")]
    [SerializeField] bool isContinueComboAttack;

    [Header("PlayerDamaged")]
    [SerializeField] float invincibilityTime = 3f;

    private bool isPlayerDamaged = false;

    //public event Action OnEvadeAction;

    protected void Start()
    {
        colliderComponents = GetComponents<Collider2D>();

        playerAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();

        isContinueComboAttack = false;
    }

    protected virtual void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameManager.Instance.isAction)
            StartAttackAnim();

        if (Input.GetMouseButton(1) && !GameManager.Instance.isAction)
            StartSkillAnim();

        //if (Input.GetKeyDown(KeyCode.C) && !GameManager.Instance.isAction)
        //    StartBackJumpAnim();
    }

    protected void FixedUpdate()
    {
        if (colliderComponents[0].enabled)
        {
            Debug.Log("Attack");
            //WideAreaAttack();
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        //KDW
        if (collision.gameObject.CompareTag("trap"))
        {
            OnPlayerDamagedByTrap(collision.transform.position);
        }

        //Enemy와 충돌하였을때 받는 데미지
        if (collision.gameObject.layer == 8)
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            OnPlayerDamaged(enemy.enemy_Attack_dmg);
        }

        //적이 쏜 탄과 충돌 시 데미지를 받고 해당 탄을 삭제
        if (collision.gameObject.tag == "EnemyBullet")
        {
            Bullet_Enemy enemyBullet = collision.gameObject.GetComponent<Bullet_Enemy>();
            OnPlayerDamaged(enemyBullet.eb_dmg);
            Destroy(collision.gameObject);
        }
    }

    //공격 입력 후에도 짧은 시간 이동이 가능하도록 하기 위함
    private void PlayerDuringAction()
    {
        GameManager.Instance.isAction = true;
    }

    private void PlayerEndAction()
    {
        GameManager.Instance.isAction = false;
    }

    //공격 시작
    protected void StartAttackAnim()
    {
        playerAnimator.SetBool("isAttack", true);
        Invoke("PlayerDuringAction", 0.05f);
    }

    //공격 종료
    protected void EndAttackAnim()
    {
        playerAnimator.SetBool("isAttack", false);
        PlayerEndAction();
    }

    //콤보 어택 체크 시작
    protected void CheckStartComboAttack()
    {
        isContinueComboAttack = false;

        Debug.Log("Attack Check Start");
        StartCoroutine(CheckComboAttack());
    }

    //콤보 어택 체크 종료
    protected void CheckEndComboAttack()
    {
        if (!isContinueComboAttack)
            EndAttackAnim();
    }

    //공격 입력 체크
    IEnumerator CheckComboAttack()
    {
        if(Input.GetKeyDown(KeyCode.C)) yield break;
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        isContinueComboAttack = true;
    }

    ////회피
    protected void StartBackJumpAnim()
    {
        playerAnimator.SetBool("isEvade", true);
        //OnEvadeAction?.Invoke();
    }

    //회피 쿨타임
    //protected void EndBackJumpAnim()
    //{
    //    playerAnimator.SetBool("isEvade", false);
    //}

    //스킬 시작
    protected virtual void StartSkillAnim()
    {
        playerAnimator.SetBool("isSkill", true);
        PlayerDuringAction();
    }

    //스킬 종료
    protected virtual void EndSkillAnim()
    {
        playerAnimator.SetBool("isSkill", false);
        PlayerEndAction();
    }

    private void WideAreaAttack()
    {
        Collider2D[] enemyColliders = new Collider2D[10];

        ContactFilter2D contactFilter = new ContactFilter2D();

        contactFilter.SetLayerMask(LayerMask.GetMask("Enemy"));

        Physics2D.OverlapCollider(colliderComponents[0], contactFilter, enemyColliders);

        foreach(Collider2D collider in enemyColliders)
        {
            EnemyController enemyCollider = collider.gameObject.GetComponent<EnemyController>();
            enemyCollider.OnDamaged(10);
        }
    }

    //피격 시 발생하는 함수 및 무적시간 부여
    //KDW
    private void OnPlayerDamaged(int dmg)
    {
        if (isPlayerDamaged)
        {
            isPlayerDamaged = true;

            playerSpriteRenderer.color = new Color(1, 1, 1, 0.4f);

            GameManager.Instance.PlayerTakeDamage(dmg);

            Invoke("InvincibilityTimeEnd", invincibilityTime);
        }
    }

    //함정에 피격 시, 이하 위와 동일
    //KDW
    private void OnPlayerDamagedByTrap(Vector2 targetPos)
    {
        Debug.Log("Trapped");

        playerSpriteRenderer.color = new Color(1, 1, 1, 0.4f);

        Invoke("InvincibilityTimeEnd", invincibilityTime);
    }

    //피격 후 무적 판정 종료
    //KDW
    private void InvincibilityTimeEnd()
    {
        isPlayerDamaged = false;

        playerSpriteRenderer.color = new Color(1, 1, 1, 1);
    }
}