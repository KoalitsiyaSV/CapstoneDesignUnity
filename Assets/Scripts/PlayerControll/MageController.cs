using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

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
    public float curShotDelay;
    public float SkillSpeed_A;
    public GameObject bulletObjA;//프리펩 변수 1
    public Transform posA;//스킬 생성 위치, 성공적



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

        if (!base.isRun && Input.GetMouseButtonDown(0))
            Fire(playerDirection);
    }

    private void FixedUpdate()
    {
        base.FixedUpdate();
    }

    void Fire(Vector2 direction)
    {
        //if (!Input.GetButton("Fire1"))
        //    return;
        if (curShotDelay < maxShotDelay)    //즉 지정 딜래이시간보다 실제 딜래이시간이 더 적으면 발사가 되지 않는다.
            return;

        GameObject bullet = Instantiate(bulletObjA/*생성한 프리팹 변수*/, posA.position/*생성위치는 플레이어위치*/, transform.rotation/*방향은 플레이어 방향으로*/);

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>(); //bullet의 rigidbody를 가져온다.
        ////가속도 항목 여기에 DaltaTime * SkillSpeed_A
        rigid.velocity = direction * SkillSpeed_A;
        //rigid.AddForce(direction * SkillSpeed_A, ForceMode2D.Impulse);//힘으로 쏜다.
        
        curShotDelay = 0;//한발 쏘고 다시 장전하는 로직
    }
    void Relode()
    {
        curShotDelay += Time.deltaTime;
    }
}