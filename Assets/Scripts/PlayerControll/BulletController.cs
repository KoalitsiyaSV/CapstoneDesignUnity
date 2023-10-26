using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("탄 속성")]
    public int dmg;
    public float DestroyTime;//사거리(시간 지나면 없어짐)
    public float acceleration;
    public float maxSpeed;

    private Vector2 currentDirection;
    private Rigidbody2D bulletRigidbody;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            DestroyObj();
        }
    }
    void FixedUpdate()
    {
        //bulletRigidbody.velocity += currentDirection * acceleration * Time.deltaTime;
    }

    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
        //currentDirection = bulletRigidbody.velocity.normalized;

        //일정 시간(DestroyTime)이 지나면 본인 삭제 메소드 호출
        Invoke("DestroyObj", DestroyTime);
    }

    void DestroyObj()
    {
        Destroy(gameObject);
    }
}
