using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject playerSprite;

    protected Rigidbody2D playerRigidbody;
    protected Animator playerAnimator;
    protected SpriteRenderer playerSpriteRenderer;
    protected Collider2D[] colliderComponents;

    [Header("test")]
    public int comboCount = 0;
    public float jumpForce = 10f;
    public float fallenSpeed = 1f;

    //달리기 관련 변수
    [Header("Run")]
    public float walkSpeed = 6f;
    public float runSpeed = 10f;
    protected float currentSpeed;
    protected float doubleTapTime = 0.2f;
    protected bool isRun = false;

    protected float xMove;

    //공격 관련
    [SerializeField] bool isContinueComboAttack = false;

    public bool canDownJump;
    public bool canJump;
    private bool isTriggerReversed = false;

    //11.09
    private bool isPlayerDamaged = false;
    // Start is called before the first frame update
    private void Awake()
    {
        playerAnimator = playerSprite.GetComponent<Animator>();
        playerSpriteRenderer = playerSprite.GetComponent<SpriteRenderer>();

        playerRigidbody = GetComponent<Rigidbody2D>();
        colliderComponents = GetComponents<Collider2D>();
    }
    protected void Start()
    {
        currentSpeed = walkSpeed;
        canJump = true;
    }
    
    // Update is called once per frame
    protected virtual void Update()
    {
        xMove = GameManager.Instance.isAction ? 0 : Input.GetAxisRaw("Horizontal");

        //점프 관련
        PlayerJump();

        //달리기 활성/비활성
        ToggleRun();
    }

    protected void FixedUpdate()
    {
        //플레이어 이동 메서드
        PlayerMovement();
    }

    //트리거 컨트롤
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            //playerAnimator.SetBool("isJump", false);
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision, true);
        }

        if (collision.gameObject.layer == 8)//Enemy와 충돌하였을때 받는 데미지
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            OnPlayer_Enemy_Damaged(enemy.enemy_Attack_dmg);
        }

        if (collision.gameObject.tag == "EnemyBullet")
        {
            Bullet_Enemy enemyBullet = collision.gameObject.GetComponent<Bullet_Enemy>();
            OnPlayer_Enemy_Damaged(enemyBullet.eb_dmg);
            Destroy(collision.gameObject);
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision, false);
            //if (colliderComponents[0].isTrigger) ReverseTrigger();
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
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Bottom") || collision.gameObject.CompareTag("Enemy") ){
            playerAnimator.SetBool("isJump", false);
            canJump = true;
        }

        //11.09 함정 부분 & Enemy 부분
        if (collision.gameObject.CompareTag ("trap"))
        {
            OnPlayer_trap_Damaged(collision.transform.position);
        }
    }
    //11.09부분 시작
    //피격시 발생하는 함수, +무적시간 부여
    void OnPlayer_Enemy_Damaged(int dmg)
    {
        if (!isPlayerDamaged)//매개 변수 변경으로 무적 및 원상복귀 구현
        {
            isPlayerDamaged = true;
            //피격시 레이어 변경(PlayerDamaged로)
            //gameObject.layer = 11;
            //힘을 줘서 피격시 밀어냄
            //playerRigidbody.AddForce(new Vector2(1, 5) * 7, ForceMode2D.Impulse);
            //색 변경
            playerSpriteRenderer.color = new Color(1, 1, 1, 0.4f);
            //채력 감소
            GameManager.Instance.PlayerTakeDamage(dmg);
            //아래로 무적시간 주고, 피격 해제
            Invoke("OffPlayerDamaged", 3);
        }
    }
    void OnPlayer_trap_Damaged(Vector2 targetPos)
    {
        Debug.Log("함정에 빠졌습니다!");
        //피격시 레이어 변경(PlayerDamaged로)
        //gameObject.layer = 11;
        //색 변경
        playerSpriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //힘을 줘서 피격시 밀어냄
        playerRigidbody.AddForce(new Vector2(1, 1) * 7, ForceMode2D.Impulse);
        //아래로 무적시간 주고, 피격 해제
        Invoke("OffPlayerDamaged", 1);

    }
    //피격판정 완료시 발생
    void OffPlayerDamaged()
    {
        isPlayerDamaged = false;
        //gameObject.layer = 7;
        playerSpriteRenderer.color = new Color(1, 1, 1, 1);
    }
    //11.09부분 끝

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision, true);
        }
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            canDownJump = false;
        }
    }

    //플레이어 이동 제어 메서드
    private void PlayerMovement()
    {
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) xMove = 0;

        float xSpeed = xMove * currentSpeed;

        TransformMoveAnim(xMove);

        // 가속도로 점프 판정
        //if (playerRigidbody.velocity.y == 0)
        //{
        //    playerAnimator.SetBool("isJump", false);
        //}

        playerRigidbody.velocity = new Vector2(xSpeed, playerRigidbody.velocity.y);
    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !playerAnimator.GetBool("isJump") && canJump)
        { //&& !playerAnimator.GetBool("isJump")
            playerRigidbody.velocity = Vector2.up * jumpForce * 1.5f;
            playerAnimator.SetBool("isJump", true);
            canJump = false;
            GameManager.Instance.PlayerTakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.S) && canDownJump)
        {
            ReverseTrigger();
            Invoke("ReverseTrigger", 0.1f);

        }
    }

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
            if(currentSpeed != walkSpeed)
            {
                currentSpeed = walkSpeed;
                isRun = !isRun;
            }
            
            playerAnimator.SetBool("isWalk", false);
            playerAnimator.SetBool("isRun", false);
        }
    }

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

    //하향 점프
    void ReverseTrigger()
    {
        colliderComponents[0].isTrigger = !colliderComponents[0].isTrigger;
        isTriggerReversed = !isTriggerReversed;
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
        Invoke("PlayerDuringAction", 0.4f);
    }

    //공격 종료
    protected void EndAttackAnim()
    {
        playerAnimator.SetBool("isAttack", false);
        Invoke("PlayerEndAction", 0.2f);
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
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        isContinueComboAttack = true;
    }

    protected virtual void StartSkillAnim()
    {
        playerAnimator.SetBool("isSkill", true);
    }

    protected virtual void EndSkillAnim()
    {
        playerAnimator.SetBool("isSkill", false);
    }
}