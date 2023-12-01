using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using UnityEngine;

using Debug = UnityEngine.Debug;
public class MageController : PlayerController
{
    //public float speed;
    //실재로 한발 쏜 다음 충전 딜레이

    [Header("AttackA")]
    public float power;
    public float startSpeed;
    public float increaseSpeed;
    public float maxSpeed;
    public float maxShotDelay;  //사용자 지정 딜레이
    public float skill_A_Delay; //기본 공격 딜레이
    public float skill_B_Delay; //범위 공격 지정 딜레이
    public float SkillSpeed_A;
    public GameObject bulletObjA;//프리펩 변수 1
    public Transform posA;//스킬 생성 위치
    //범위 공격을 위한 변수
    public Transform BoxPos;
    public Vector2 BoxSize;
    public int Range_Attack_dmg;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();   
    }
    
    // Update is called once per frame
    void Update()
    {
        base.Update();

        //Fire(Vector2.right);
        Relode();

        Vector2 playerDirection = Vector2.right;// 기본값은 오른쪽 방향
       
        if(transform.localScale.x < 0)
        {
            playerDirection = Vector2.left;
        }else if(transform.localScale.x > 0)
        {
            playerDirection = Vector2.right;
        }

        //Attack_Fire part
        if (!base.isRun && Input.GetMouseButtonDown(0))
            Fire(playerDirection);

        //Attack_Range part
        if (Input.GetMouseButtonDown(1))
        {
            Range_Attack();
        }

    }

    private void FixedUpdate()
    {
        base.FixedUpdate();
    }

    //범위 공격 구현
    void Range_Attack()
    {
        if (skill_B_Delay < maxShotDelay+1)//범위 공격 delay는 0.2가 기본 공격 딜레이인거 고려하여 넣어야함
            return;
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(BoxPos.position, BoxSize, 0);//point = 박스 생성 위치, size= 박스크기, angle = 박스의 회전
        foreach (Collider2D collider in collider2Ds)//collider2Ds의 값중 Enemy만 골라냄
        {
            if (collider.CompareTag("Enemy"))
            {
                //Debug.Log(collider.tag);
                EnemyController collider_Enemy = collider.gameObject.GetComponent<EnemyController>();
                collider_Enemy.OnDamaged(Range_Attack_dmg);
            }
        }
        skill_B_Delay = 0;
    }

    void Fire(Vector2 direction)
    {
        //if (!Input.GetButton("Fire1"))
        //    return;
        if (skill_A_Delay < maxShotDelay)    //즉 지정 딜래이시간보다 실제 딜래이시간이 더 적으면 발사가 되지 않는다.
            return;

        GameObject bullet = Instantiate(bulletObjA/*생성한 프리팹 변수*/, posA.position/*생성위치는 플레이어위치*/, transform.rotation/*방향은 플레이어 방향으로*/);

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>(); //bullet의 rigidbody를 가져온다.
        ////가속도 항목 여기에 DaltaTime * SkillSpeed_A
        rigid.velocity = direction * SkillSpeed_A;
        //rigid.AddForce(direction * SkillSpeed_A, ForceMode2D.Impulse);//힘으로 쏜다.
        
        skill_A_Delay = 0;//한발 쏘고 다시 장전하는 로직
    }
    void Relode()
    {
        skill_A_Delay += Time.deltaTime;
        skill_B_Delay += Time.deltaTime;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(BoxPos.position, BoxSize);
    }
}