using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

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
    public float curShotDelay;
    public float SkillSpeed_A;
    public GameObject bulletObjA;//������ ���� 1
    public Transform posA;//��ų ���� ��ġ, ������



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
        if (curShotDelay < maxShotDelay)    //�� ���� �����̽ð����� ���� �����̽ð��� �� ������ �߻簡 ���� �ʴ´�.
            return;

        GameObject bullet = Instantiate(bulletObjA/*������ ������ ����*/, posA.position/*������ġ�� �÷��̾���ġ*/, transform.rotation/*������ �÷��̾� ��������*/);

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>(); //bullet�� rigidbody�� �����´�.
        ////���ӵ� �׸� ���⿡ DaltaTime * SkillSpeed_A
        rigid.velocity = direction * SkillSpeed_A;
        //rigid.AddForce(direction * SkillSpeed_A, ForceMode2D.Impulse);//������ ���.
        
        curShotDelay = 0;//�ѹ� ��� �ٽ� �����ϴ� ����
    }
    void Relode()
    {
        curShotDelay += Time.deltaTime;
    }
}