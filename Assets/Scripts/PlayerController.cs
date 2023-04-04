using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;
    private SpriteRenderer playerSpriteRenderer;

    public float speed = 8f;

    // Start is called before the first frame update
    void Start() {
        playerRigidbody = GetComponent<Rigidbody2D>(); 
        playerAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void FixedUpdate() {
        float xMove = Input.GetAxisRaw("Horizontal");

        float xSpeed = xMove * speed;

        Vector3 newVelocity = new Vector3(xSpeed, 0f, 0f);
        playerRigidbody.velocity = newVelocity;
    }
}
