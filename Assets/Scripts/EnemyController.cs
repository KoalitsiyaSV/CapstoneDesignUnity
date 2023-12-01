using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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

    [Header("몬스터")]
    public int ememy_Type;
    public int nextMove;    //다시 이동하는데 걸리는 시간
    public float enemy_Life;
    public float raycastLength;
    //11/29test
    //public Transform Attack_Pos;
    //public Vector2 Attack_Size;
    public float attackRayLength;
    public float enemy_Move_Speed;
    public int enemy_Attack_dmg;
    //Long Distance Attack
    //public float enemy_Distance;
    public float enemy_AttackDistance;//Enemy 공격사거리
    public float enemy_Attack_Delay;  //공격 딜레이
    public float max_Attack_Delay;    //지정 최고 공격 딜레이
    public GameObject bullet_E;         
    public float bullet_E_Speed;
    public Transform bullet_Pos;
    
    //public Sprite[] sprites;//추후 Enemy추가시 사용할 Sprite배열

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        //Invoke("Think", 2);//추후 에너미 타입에 따라 다르게 할거임
    }

    //void Update() //매 프레임마다 호출
    //{
    //   
    //}

    void FixedUpdate()//물리 엔진 업데이트 주기와 동기화 => 주로 물리 엔진 관련 작업에 사용됨 => 시간 간격이 일정하게 유지되며, 프레임 레이트에 영향을 받지 않음
    {
        Destroy_Enemy();
        Enemy_Relode();

        Vector2 EnemyDirection = Vector2.left;
        if (transform.localScale.x < 0)
        {
            EnemyDirection = Vector2.right;
        }
        else if (transform.localScale.x > 0)
        {
            EnemyDirection = Vector2.left;
        }

        Enemy_Attack(EnemyDirection);
        if (ememy_Type == 1)
        {
            //이걸로 움직이는걸 계속 업데이트 하면서 이동시킴, Move
            rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

            //지능 높이기 부분, PlatForm Check
            Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);//이걸로 위치세부 지정 가능
            //RayCast 형성
            Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
            //레이에 맞은 것의 정보를 받음
            RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, raycastLength, LayerMask.GetMask("Default")); //Default로 임시 변경
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
        TransformAnim();
        Invoke("Think", 2); //재귀함수
    }

    private void TransformAnim()
    {
        if (nextMove > 0)
        {
            transform.localScale = new Vector2(1, 1);
            anim.SetBool("isWalk", true);
        }
        else if (nextMove < 0)
        {
            transform.localScale = new Vector2(-1, 1);
            anim.SetBool("isWalk", true);
        }
        else if (nextMove == 0)
        {
            anim.SetBool("isWalk", false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Player is Detected");
            //OnDamaged(collision.transform.position);
            //OnDamaged();
        }
    }
    
    void OnHit(int dmg)//맞췄을때 발생함
    {
        enemy_Life -= dmg;

        //충돌시 반짝효과
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        Invoke("OffDamaged", 0.2f);
    }
    void Destroy_Enemy()
    {
        if (enemy_Life <= 0)
        {
            Destroy(gameObject);//본인 객체 파괴
        }
    }

    void OnTriggerEnter2D(Collider2D collision)//충돌 발생시 Enemy의 Enemy_Life 감소
    {
        if (collision.gameObject.tag == "Bullet")
        {
            BulletController bullet = collision.gameObject.GetComponent<BulletController>();  //이 부분이 다른 스크립트의 변수 호출해서 값 지정해 넣고 하는 부분 제일중요!
            OnHit(bullet.dmg);
            Destroy(collision.gameObject);//플레이어의 총알을 삭제한다
        }
    }

    public void OnDamaged(int dmg)
    {
        //dmg만큼 체력감소
        enemy_Life -= dmg;
        //gameObject.layer = ; //몬스터 피격 레이어

        //피격시 색을 변경시킴
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //해당 시간 후에 OffDamaged발생
        Invoke("OffDamaged", 1);
        //anim.SetBool("Enemy_Damaged", true);
    }
    void OffDamaged()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
        //anim.SetBool("Enemy_Damaged", false);
    }

    void Enemy_Attack(Vector2 direction)
    {
        //Enemy is detect Player
        Vector2 rayStartPos = new Vector2(rigid.position.x - attackRayLength/2, rigid.position.y-0.4f);
        //Debug.DrawRay(rayStartPos, Vector3.right * attackRayLength, Color.red);
        RaycastHit2D rayHit_attack = Physics2D.Raycast(rayStartPos, Vector3.right, attackRayLength, LayerMask.GetMask("Player")); //Layer가 Player인 오브젝트 탐지
        if (rayHit_attack.collider != null)
        {
            if (Vector2.Distance(transform.position, rayHit_attack.collider.transform.position) < enemy_AttackDistance)
            {
                //플레이어의 위치에 및 좌표에 따른 방향 설정
                if((transform.position.x - rayHit_attack.collider.transform.position.x)> 0)
                {
                    transform.localScale = new Vector2(1, 1);
                }
                else
                {
                    transform.localScale = new Vector2(-1, 1);
                }
                //shot bullet
                anim.SetBool("Enemy_Attack", true);
                Fire_Enemy(direction);
            }
            else
            {
                //Enemy follow Player
                Vector3 targetPos = new Vector3(rayHit_attack.collider.transform.position.x, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * enemy_Move_Speed);
            }
        }
    }
    //Enemy의 원거리 공격 발사
    void Fire_Enemy(Vector2 direction) {
        if (enemy_Attack_Delay < max_Attack_Delay)
            return;

        Bullet_Enemy bullet_Enemy = Instantiate(bullet_E, bullet_Pos.position, transform.rotation).GetComponent<Bullet_Enemy>();
        Rigidbody2D rigid_B = bullet_Enemy.GetComponent<Rigidbody2D>();
        SpriteRenderer sprite_B = bullet_Enemy.GetComponent<SpriteRenderer>();

        //총알의SpriteRenderer를 통해 보이는 이미지를 그대로 혹은 반전하여 화살이 날아가는것을 표현합니다. 
        if (direction == Vector2.left)
        {
            sprite_B.flipX = true;
        }
        else
        {
            sprite_B.flipX = false;
        }
        bullet_Enemy.eb_dmg = enemy_Attack_dmg;
        rigid_B.velocity = direction * bullet_E_Speed;
        enemy_Attack_Delay = 0;
        anim.SetBool("Enemy_Attack", false);
    }

    void Enemy_Relode()
    {
        enemy_Attack_Delay += Time.deltaTime;
    }
}