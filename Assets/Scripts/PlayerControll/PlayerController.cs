using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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

    private float lastAttackTime = 0;
    //private float maxComboCount = 2;
    private float maxComboDelay = 0.1f;

    public bool canDownJump;
    public bool canJump;
    private bool isTriggerReversed = false;

    // Start is called before the first frame update
    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        colliderComponents = GetComponents<Collider2D>();
    }
    protected void Start()
    {
        currentSpeed = walkSpeed;
        canJump = true;
    }

    // Update is called once per frame
    protected void Update()
    {
        //점프 관련
        PlayerJump();

        //달리기 활성/비활성
        ToggleRun();
        //}

        //if(Input.GetMouseButtonDown(0) && playerAnimator.GetBool("isAttack")) {
        //    playerAnimator.SetBool("isAttack2", true);
        //}
        //else if (Input.GetMouseButtonDown(0)) {
        //playerAnimator.SetBool("isAttack", true);
        //}

        //콤보 어택 관련
        //if (Time.time - lastAttackTime > maxComboDelay)
        //{
        //    playerAnimator.SetBool("isAttack", false);
        //    playerAnimator.SetBool("isAttack2", false);
        //    comboCount = 0;
        //}

        //if (Input.GetMouseButtonDown(0) && comboCount < 2)
        //{
        //    lastAttackTime = Time.time;

        //    comboCount++;

        //    if(comboCount == 1)
        //    {
        //        playerAnimator.SetBool("isAttack", true);
        //        maxComboDelay = playerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length - 0.1f;
        //    }
        //    else if(comboCount == 2)
        //    {
        //        playerAnimator.SetBool("isAttack", false);
        //        playerAnimator.SetBool("isAttack2", true);
        //        maxComboDelay = playerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length - 0.1f;
        //    }
        //}
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
    }

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
        float xMove = Input.GetAxis("Horizontal");

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
}


// ComboAttack Test1

//    public void return1()
//    {
//        if(comboCount >= 2)
//        {
//            playerAnimator.SetBool("isAttack2", true);
//        }
//        else
//        {
//            playerAnimator.SetBool("isAttack", false);
//            comboCount = 0;
//        }
//    }

//    public void return2()
//    {
//        playerAnimator.SetBool("isAttack", false);
//        playerAnimator.SetBool("isAttack2", false);
//        comboCount = 0;
//    }
//}
