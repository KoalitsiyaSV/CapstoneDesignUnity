using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("맞았습니다");
            OnDamaged(collision.transform.position);
        }
    }

    void OnDamaged(Vector2 targetPos)
    {
        //gameObject.layer = ; //몬스터 피격 레이어

        //피격시 색을 변경시킴
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //해당 시간 후에 OffDamaged발생
        Invoke("OffDamaged", 1);
    }
    void OffDamaged()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}