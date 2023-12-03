using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] GameObject playerObj;

    private Rigidbody2D playerRigidbody;
    private Collider2D[] colliderComponents;

    private Animator playerAnimator;
    private SpriteRenderer playerSpriteRenderer;

    [Header("Test")]
    [SerializeField] float jumpForce = 10f;
    //[SerializeField] float fallenSpeed = 1f;

    [Header("Run Option")]
    [SerializeField] float walkSpeed = 6f;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float currentSpeed;
    //[SerializeField] float doubleTapGap = 0.2f;
    [SerializeField] bool isRun = false;

    private float xMove;

    [Header("Battle")]
    [SerializeField] bool isContinueComboAttack = false;
    [SerializeField] bool isPlayerDamaged = false;

    [Header("Jump")]
    [SerializeField] bool canDownJump;
    [SerializeField] bool canJump;
    [SerializeField] bool isTriggerReversed;

    [Header("BackJump")]
    [SerializeField] float evadeForceX = 5f;
    [SerializeField] float evadeForceY = 12f;
    [SerializeField] float backJumpDuration = 2f;
    [SerializeField] float backJumpCoolDown = 3f;
    [SerializeField] bool canBackJump = true;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        colliderComponents = GetComponents<Collider2D>();

        playerAnimator = playerObj.GetComponent<Animator>();
        playerSpriteRenderer = playerObj.GetComponent<SpriteRenderer>();

        currentSpeed = walkSpeed;
        canJump = true;
    }

    // Update is called once per frame
    private void Update()
    {
        xMove = GameManager.Instance.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        PlayerJump();
        ToggleRun();
    }

    private void FixedUpdate()
    {
        //플레이어 이동 메서드
        PlayerMovement();

        //플레이어 회피 메서드
        OnBackJumpAction();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FieldItem"))
        {
            Debug.Log("Item Detected");
            
            FieldItem fieldItem = collision.GetComponent<FieldItem>();

            if (fieldItem.canPickUp)
            {
                fieldItem.item.use();

                fieldItem.DestroyItem();
            }
        }

        //if (collision.CompareTag("Platform"))
        //{
        //    //playerAnimator.SetBool("isJump", false);
        //    Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision, true);
        //}

        //if (collision.gameObject.layer == 8)//Enemy와 충돌하였을때 받는 데미지
        //{
        //    EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        //    OnPlayer_Enemy_Damaged(enemy.enemy_Attack_dmg);
        //}

        //if (collision.gameObject.tag == "EnemyBullet")
        //{
        //    Bullet_Enemy enemyBullet = collision.gameObject.GetComponent<Bullet_Enemy>();
        //    OnPlayer_Enemy_Damaged(enemyBullet.eb_dmg);
        //    Destroy(collision.gameObject);
        //}
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision, true);
        }

        if (collision.CompareTag("FieldItem"))
        {
            Debug.Log("Item Stay Detected");

            FieldItem fieldItem = collision.GetComponent<FieldItem>();

            if (fieldItem.canPickUp)
            {
                fieldItem.item.use();

                fieldItem.DestroyItem();
            }
        }
    }

    //콜리젼 컨트롤
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            canDownJump = true;
        }
        if (collision.gameObject.CompareTag("Bottom"))
        {
            canDownJump = false;
        }
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Bottom") || collision.gameObject.CompareTag("Enemy"))
        {
            playerAnimator.SetBool("isJump", false);
            canJump = true;
            GameManager.Instance.isAction = false;
        }
    }

    //플레이어 이동 제어 메서드
    private void PlayerMovement()
    {
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) xMove = 0;

        float xSpeed = xMove * currentSpeed;

        TransformMoveAnim(xMove);

        // 가속도로 점프 판정
        //if (playerRigidbody.velocity.y == 0)
        //{
        //    playerAnimator.SetBool("isJump", false);
        //}

        if(canJump)
            playerRigidbody.velocity = new Vector2(xSpeed, playerRigidbody.velocity.y);
    }

    //이동 방향에 따른 플레이어 스프라이트 방향 전환 및 애니메이션 파라미터 전달
    private void TransformMoveAnim(float xMove)
    {
        if (!isRun)
        {
            if (xMove > 0)
            {
                transform.localScale = new Vector2(1, 1);
                playerAnimator.SetBool("isWalk", true);
            }
            else if (xMove < 0)
            {
                transform.localScale = new Vector2(-1, 1);
                playerAnimator.SetBool("isWalk", true);
            }
        }
        else
        {
            if (xMove > 0)
            {
                transform.localScale = new Vector2(1, 1);
                playerAnimator.SetBool("isRun", true);
            }
            else if (xMove < 0)
            {
                transform.localScale = new Vector2(-1, 1);
                playerAnimator.SetBool("isRun", true);
            }
        }

        if (xMove == 0)
        {
            if (currentSpeed != walkSpeed)
            {
                currentSpeed = walkSpeed;
                isRun = !isRun;
            }

            playerAnimator.SetBool("isWalk", false);
            playerAnimator.SetBool("isRun", false);
        }
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            canDownJump = false;
        }
    }

    //플레이어 점프 및 하향 기능
    private  void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !playerAnimator.GetBool("isJump") && canJump)
        { //&& !playerAnimator.GetBool("isJump")
            playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //playerRigidbody.velocity = Vector2.up * jumpForce * 1.5f;
            playerAnimator.SetBool("isJump", true);
            canJump = false;
            GameManager.Instance.PlayerTakeDamage(10);

            GameManager.Instance.isAction = true;
        }

        if (Input.GetKeyDown(KeyCode.S) && canDownJump)
        {
            ReverseTrigger();

            Invoke("ReverseTrigger", 0.1f);

        }
    }

    //달리기 기능
    private void ToggleRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canJump)
        {
            if (currentSpeed == walkSpeed)
            {
                currentSpeed = runSpeed;
                isRun = !isRun;
            }
            else
            {
                currentSpeed = walkSpeed;
                isRun = !isRun;
                playerAnimator.SetBool("isRun", false);
            }
        }
    }

    private void OnBackJumpAction()
    {
        if (Input.GetKeyDown(KeyCode.C) && !playerAnimator.GetBool("isEvade") && canBackJump && (!GameManager.Instance.isAction || playerAnimator.GetBool("isAttack")))
        {
            playerAnimator.SetBool("isAttackToEvade", true);
            playerAnimator.SetBool("isEvade", true);
            canJump = false;
            GameManager.Instance.isAction = true;

            playerAnimator.SetBool("isAttack", false);

            playerRigidbody.AddForce(Vector2.up * evadeForceY, ForceMode2D.Impulse);

            if (transform.localScale.x == 1)
                StartCoroutine(BackJump(-10f));
            else if (transform.localScale.x == -1)
                StartCoroutine(BackJump(10f));
        }
    }

    //하향 점프를 위한 충돌 판정 반전 트리거
    void ReverseTrigger()
    {
        colliderComponents[0].isTrigger = !colliderComponents[0].isTrigger;
        isTriggerReversed = !isTriggerReversed;
    }

    IEnumerator BackJump(float xSpeed)
    {
        canBackJump = false;

        playerRigidbody.velocity = new Vector2(xSpeed, playerRigidbody.velocity.y);

        

        yield return new WaitForSeconds(backJumpDuration);
        playerRigidbody.velocity = new Vector2(0, playerRigidbody.velocity.y);
        playerAnimator.SetBool("isEvade", false);
        playerAnimator.SetBool("isAttackToEvade", false);
        GameManager.Instance.isAction = false;

        yield return new WaitForSeconds(backJumpCoolDown);

        canBackJump = true;
    }
}