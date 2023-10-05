using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;
    private SpriteRenderer playerSpriteRenderer;
    private Collider2D[] colliderComponents;

    [Header("test")]
    public int comboCount = 0;
    public float jumpForce = 10f;
    public float fallenSpeed = 1f;

    //щ━湲 愿� 蹂
    [Header("Run")]
    private float walkSpeed = 6f;
    private float runSpeed = 10f;
    private float currentSpeed;
    private float doubleTapTime = 0.2f;
    private bool isRun = false;

    private float lastAttackTime = 0;
    //private float maxComboCount = 2;
    private float maxComboDelay = 0.1f;

    public bool canDownJump;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        colliderComponents = GetComponents<Collider2D>();

        currentSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //� 肄
        //if (!playerAnimator.GetBool("isAttack")) {
            if (Input.GetKeyDown(KeyCode.Space) ) { //&& !playerAnimator.GetBool("isJump")
                playerRigidbody.velocity = Vector2.up * jumpForce * 1.5f;
                playerAnimator.SetBool("isJump", true);
            }

            if (Input.GetKeyDown(KeyCode.S)) {
                ReverseTrigger();
                Invoke("ReverseTrigger", fallenSpeed);
                //canDownJump = false;
            }
        //}

        //if(Input.GetMouseButtonDown(0) && playerAnimator.GetBool("isAttack")) {
        //    playerAnimator.SetBool("isAttack2", true);
        //}
        //else if (Input.GetMouseButtonDown(0)) {
        //playerAnimator.SetBool("isAttack", true);
        //}

        //肄ㅻ낫 댄 愿� 肄
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

    private void FixedUpdate()
    {
        //罹由� 대 肄
        PlayerMovement();
    }

    //寃쎌щ 듦낵 愿�, � 諛 怨듬 
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            canDownJump = true;
            playerAnimator.SetBool("isJump", false);
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision, true);
        }
        if (collision.CompareTag("Bottom"))
        {
            canDownJump = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision, true);  
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision, false);
            //if (colliderComponents[0].isTrigger) ReverseTrigger();
        }
    }

    //�댁 대 愿�
    private void PlayerMovement()
    {
        float xMove = Input.GetAxisRaw("Horizontal");

        ToggleRun();

        float xSpeed = xMove * currentSpeed;

        TransformMoveAnim(xMove);

        // 媛濡 �  諛蹂
        //if (playerRigidbody.velocity.y == 0)
        //{
        //    playerAnimator.SetBool("isJump", false);
        //}

        playerRigidbody.velocity = new Vector2(xSpeed, playerRigidbody.velocity.y);
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
            playerAnimator.SetBool("isWalk", false);
            playerAnimator.SetBool("isRun", false);
        }
    }

    private void ToggleRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
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
            }
        }
    }

    //�댁 諛 � 몃━嫄 諛�
    void ReverseTrigger()
    {
        colliderComponents[0].isTrigger = !colliderComponents[0].isTrigger;
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