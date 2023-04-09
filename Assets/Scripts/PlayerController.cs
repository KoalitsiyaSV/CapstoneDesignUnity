using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;
    private SpriteRenderer playerSpriteRenderer;

    public float speed = 8f;
    public float jumpForce = 10f;

    // Start is called before the first frame update
    void Start() {
        playerRigidbody = GetComponent<Rigidbody2D>(); 
        playerAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    // Update is called once per frame
    void Update() {
            if (Input.GetKeyDown(KeyCode.Space) && !playerAnimator.GetBool("isJump")) {
                playerRigidbody.velocity = Vector2.up * jumpForce * 1.5f;
                playerAnimator.SetBool("isJump", true);
            }
        }

    private void FixedUpdate()
    {
        float xMove = Input.GetAxisRaw("Horizontal");

        float xSpeed = xMove * speed;

        if(xMove > 0) {
            transform.localScale = new Vector2(1, 1);
            playerAnimator.SetBool("isRun", true);
        }
        else if(xMove < 0) {
            transform.localScale = new Vector2(-1, 1);
            playerAnimator.SetBool("isRun", true);
        }
        else {
            playerAnimator.SetBool("isRun", false);
        }

        // 가속도로 점프 상태 반별
        if (playerRigidbody.velocity.y == 0)
        {
            playerAnimator.SetBool("isJump", false);
        }

        playerRigidbody.velocity = new Vector2(xMove * speed, playerRigidbody.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Slope")) {
            playerAnimator.SetBool("isJump", false);
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision, true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Slope"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Slope")) {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision, false);
        }
    }
}