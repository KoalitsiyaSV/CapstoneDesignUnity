using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Enemy : MonoBehaviour
{
    [Header("EnemyBullet")]
    public int eb_dmg;
    public float DestroyTime;//��Ÿ�(�ð� ������ ������)
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

        //���� �ð�(DestroyTime)�� ������ ���� ���� �޼ҵ� ȣ��
        Invoke("DestroyObj", DestroyTime);
    }

    void DestroyObj()
    {
        Destroy(gameObject);
    }
}
