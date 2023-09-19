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
    public int jumpCount = 0; //LJHClient
    public float speed = 8f;
    public float jumpForce = 10f;
    public float fallenSpeed = 1f;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerAnimator.GetBool("isAttack")) //LJHCLient
        {
            if (Input.GetKeyDown(KeyCode.Space) && jumpCount != 2)
            {
                playerRigidbody.velocity = Vector2.up * jumpForce * 1.5f;
                playerAnimator.SetBool("isJump", true);
                jumpCount += 1;
            }
            

            if (Input.GetKeyDown(KeyCode.S))
            {
                ReverseTrigger();
                Invoke("ReverseTrigger", fallenSpeed);
                //canDownJump = false;
            }
        }

        /*if (!playerAnimator.GetBool("isAttack")) {
            if (Input.GetKeyDown(KeyCode.Space) && !playerAnimator.GetBool("isJump"))
            {
                playerRigidbody.velocity = Vector2.up * jumpForce * 1.5f;
                playerAnimator.SetBool("isJump", true);
            }

            if (Input.GetKeyDown(KeyCode.S)) {
                ReverseTrigger();
                Invoke("ReverseTrigger", fallenSpeed);
                //canDownJump = false;
            }
        }*/

       
        //if(Input.GetMouseButtonDown(0) && playerAnimator.GetBool("isAttack")) {
        //    playerAnimator.SetBool("isAttack2", true);
        //}
        //else if (Input.GetMouseButtonDown(0)) {
        //playerAnimator.SetBool("isAttack", true);
        //}

        if (Time.time - lastAttackTime > maxComboDelay)
        {
            playerAnimator.SetBool("isAttack", false);
            playerAnimator.SetBool("isAttack2", false);
            comboCount = 0;
        }

        if (Input.GetMouseButtonDown(0) && comboCount < 2)
        {
            lastAttackTime = Time.time;

            comboCount++;

            if(comboCount == 1)
            {
                playerAnimator.SetBool("isAttack", true);
                maxComboDelay = playerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length - 0.1f;
            }
            else if(comboCount == 2)
            {
                playerAnimator.SetBool("isAttack", false);
                playerAnimator.SetBool("isAttack2", true);
                maxComboDelay = playerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length - 0.1f;
            }
            //    if(comboCount == 1)
            //    {
            //        playerAnimator.SetBool("isAttack", true);
            //    }
            //    comboCount = Mathf.Clamp(comboCount, 0, 3);
        }
    }

    private void FixedUpdate()
    {
        if (!playerAnimator.GetBool("isAttack") && !playerAnimator.GetBool("isAttack2"))
        {
            float xMove = Input.GetAxisRaw("Horizontal");

            float xSpeed = xMove * speed;

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
            else
            {
                playerAnimator.SetBool("isRun", false);
            }

            // 가속도로 점프 상태 반별
            if (playerRigidbody.velocity.y == 0)
            {
                playerAnimator.SetBool("isJump", false);
                jumpCount = 0; // LJHClient
            }

            playerRigidbody.velocity = new Vector2(xMove * speed, playerRigidbody.velocity.y);
        }
    }

    //경사로 통과 관련, 수정 및 공부 필요할듯
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

    //플레이어 발 판정 트리거 반전
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