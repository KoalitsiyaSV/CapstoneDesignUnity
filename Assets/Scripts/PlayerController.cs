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
    public bool isGrounded = true;

    // Start is called before the first frame update
    void Start() {
        playerRigidbody = GetComponent<Rigidbody2D>(); 
        playerAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    // Update is called once per frame
    void Update() {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
                playerRigidbody.velocity = Vector2.up * jumpForce;
                isGrounded = false;
            }
        }

    private void FixedUpdate()
    {
        float xMove = Input.GetAxisRaw("Horizontal");

        float xSpeed = xMove * speed;

        playerRigidbody.velocity = new Vector2(xMove * speed, playerRigidbody.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Slope")) {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision, true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Slope")) {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Slope")) {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision, false);
        }
    }
}