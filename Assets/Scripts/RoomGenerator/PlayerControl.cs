using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// юс╫ц
public class PlayerControl : MonoBehaviour
{
	private float moveSpeed = 20f; 
	private float jumpHeight = 10f; 
	Rigidbody2D rigid;
	void Awake() {
		rigid = GetComponent<Rigidbody2D>();
		rigid.freezeRotation = true;
	}

	void FixedUpdate() {
		move();
	}
    private void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			jump();
		}
	}

    void move() {
		Vector3 moveVelocity = Vector3.zero;

		if (Input.GetAxisRaw("Horizontal") < 0) {
			moveVelocity = Vector3.left;
		}

		else if (Input.GetAxisRaw("Horizontal") > 0) {
			moveVelocity = Vector3.right;
		}

		transform.position += moveVelocity * moveSpeed * Time.deltaTime;
	}
	void jump() {
		rigid.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
	}
}
