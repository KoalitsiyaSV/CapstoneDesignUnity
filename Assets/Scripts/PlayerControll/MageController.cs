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
    //����� �ѹ� �� ���� ���� ������

    [Header("AttackA")]
    public float power;
    public float startSpeed;
    public float increaseSpeed;
    public float maxSpeed;
    public float maxShotDelay;  //����� ���� ������
    public float skill_A_Delay; //�⺻ ���� ������
    public float skill_B_Delay; //���� ���� ���� ������
    public float SkillSpeed_A;
    public GameObject bulletObjA;//������ ���� 1
    public Transform posA;//��ų ���� ��ġ
    //���� ������ ���� ����
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

        Vector2 playerDirection = Vector2.right;// �⺻���� ������ ����
       
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

    //���� ���� ����
    void Range_Attack()
    {
        if (skill_B_Delay < maxShotDelay+1)//���� ���� delay�� 0.2�� �⺻ ���� �������ΰ� �����Ͽ� �־����
            return;
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(BoxPos.position, BoxSize, 0);//point = �ڽ� ���� ��ġ, size= �ڽ�ũ��, angle = �ڽ��� ȸ��
        foreach (Collider2D collider in collider2Ds)//collider2Ds�� ���� Enemy�� ���
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
        if (skill_A_Delay < maxShotDelay)    //�� ���� �����̽ð����� ���� �����̽ð��� �� ������ �߻簡 ���� �ʴ´�.
            return;

        GameObject bullet = Instantiate(bulletObjA/*������ ������ ����*/, posA.position/*������ġ�� �÷��̾���ġ*/, transform.rotation/*������ �÷��̾� ��������*/);

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>(); //bullet�� rigidbody�� �����´�.
        ////���ӵ� �׸� ���⿡ DaltaTime * SkillSpeed_A
        rigid.velocity = direction * SkillSpeed_A;
        //rigid.AddForce(direction * SkillSpeed_A, ForceMode2D.Impulse);//������ ���.
        
        skill_A_Delay = 0;//�ѹ� ��� �ٽ� �����ϴ� ����
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