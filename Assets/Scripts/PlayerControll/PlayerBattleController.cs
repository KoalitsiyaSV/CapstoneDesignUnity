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

        //Enemy�� �浹�Ͽ����� �޴� ������
        if (collision.gameObject.layer == 8)
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            OnPlayerDamaged(enemy.enemy_Attack_dmg);
        }

        //���� �� ź�� �浹 �� �������� �ް� �ش� ź�� ����
        if (collision.gameObject.tag == "EnemyBullet")
        {
            Bullet_Enemy enemyBullet = collision.gameObject.GetComponent<Bullet_Enemy>();
            OnPlayerDamaged(enemyBullet.eb_dmg);
            Destroy(collision.gameObject);
        }
    }

    //���� �Է� �Ŀ��� ª�� �ð� �̵��� �����ϵ��� �ϱ� ����
    private void PlayerDuringAction()
    {
        GameManager.Instance.isAction = true;
    }

    private void PlayerEndAction()
    {
        GameManager.Instance.isAction = false;
    }

    //���� ����
    protected void StartAttackAnim()
    {
        playerAnimator.SetBool("isAttack", true);
        Invoke("PlayerDuringAction", 0.05f);
    }

    //���� ����
    protected void EndAttackAnim()
    {
        playerAnimator.SetBool("isAttack", false);
        PlayerEndAction();
    }

    //�޺� ���� üũ ����
    protected void CheckStartComboAttack()
    {
        isContinueComboAttack = false;

        Debug.Log("Attack Check Start");
        StartCoroutine(CheckComboAttack());
    }

    //�޺� ���� üũ ����
    protected void CheckEndComboAttack()
    {
        if (!isContinueComboAttack)
            EndAttackAnim();
    }

    //���� �Է� üũ
    IEnumerator CheckComboAttack()
    {
        if(Input.GetKeyDown(KeyCode.C)) yield break;
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        isContinueComboAttack = true;
    }

    ////ȸ��
    protected void StartBackJumpAnim()
    {
        playerAnimator.SetBool("isEvade", true);
        //OnEvadeAction?.Invoke();
    }

    //ȸ�� ��Ÿ��
    //protected void EndBackJumpAnim()
    //{
    //    playerAnimator.SetBool("isEvade", false);
    //}

    //��ų ����
    protected virtual void StartSkillAnim()
    {
        playerAnimator.SetBool("isSkill", true);
        PlayerDuringAction();
    }

    //��ų ����
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

    //�ǰ� �� �߻��ϴ� �Լ� �� �����ð� �ο�
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

    //������ �ǰ� ��, ���� ���� ����
    //KDW
    private void OnPlayerDamagedByTrap(Vector2 targetPos)
    {
        Debug.Log("Trapped");

        playerSpriteRenderer.color = new Color(1, 1, 1, 0.4f);

        Invoke("InvincibilityTimeEnd", invincibilityTime);
    }

    //�ǰ� �� ���� ���� ����
    //KDW
    private void InvincibilityTimeEnd()
    {
        isPlayerDamaged = false;

        playerSpriteRenderer.color = new Color(1, 1, 1, 1);
    }
}