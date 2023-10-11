using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;
public class EnemyController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator anim;
    Rigidbody2D rigid;

    //김동우 개발 Part
    public int EnemyType;
    public int nextMove;    //다시 이동하는데 걸리는 시간
    public float Enemy_Life;
    //public Sprite[] sprites;//추후 Enemy추가시 사용할 Sprite배열

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        //Invoke("Think", 2);
    }

    /*void Update() //매 프레임마다 호출
    {

    }*/

    void FixedUpdate()//물리 엔진 업데이트 주기와 동기화 => 주로 물리 엔진 관련 작업에 사용됨 => 시간 간격이 일정하게 유지되며, 프레임 레이트에 영향을 받지 않음
    {
        if (EnemyType == 1)
        {
            //이걸로 움직이는걸 계속 업데이트 하면서 이동시킴, Move
            rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

            //지능 높이기 부분, PlatForm Check
            Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);//이걸로 위치세부 지정 가능
            //RayCast 형성
            Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
            //레이에 맞은 것의 정보를 받음
            RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider == null)    //앞에 Platform이름의 타일이 없다(null)
            {
                //Debug.Log("가지마용");
                nextMove *= -1;
                CancelInvoke();
                Invoke("Think", 2);
            }
        }
    }
    void Think()
    {
        nextMove = Random.Range(-1, 2); //랜덤에서 최솟값은 상관이 없지만, 최대값은 목표인 1이 아닌 2가 들어가야함
        //float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", 2); //재귀함수
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("맞았습니다");
            OnDamaged(collision.transform.position);
        }
    }
    
    void OnHit(int dmg)//맞췄을때 발생함
    {
        Enemy_Life -= dmg;

        //충돌시 반짝효과
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        Invoke("OffDamaged", 0.2f);

        if (Enemy_Life <= 0)
        {
            Destroy(gameObject);//본인 객체 파괴
        }
    }

    void OnTriggerEnter2D(Collider2D collision)//충돌 발생시 Enemy의 Enemy_Life 감소
    {
        if (collision.gameObject.tag == "Bullet")
        {
            SkillController bullet = collision.gameObject.GetComponent<SkillController>();  //이 부분이 다른 스크립트의 변수 호출해서 값 지정해 넣고 하는 부분 제일중요!
            OnHit(bullet.dmg);
            Destroy(collision.gameObject);//플레이어의 총알을 삭제한다 이때, collision개념 잘 파악
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