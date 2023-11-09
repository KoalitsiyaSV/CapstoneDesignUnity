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

    //�赿�� ���� Part
    [Header("����")]
    public int EnemyType;
    public int nextMove;    //�ٽ� �̵��ϴµ� �ɸ��� �ð�
    public float Enemy_Life;
    public float raycastLength;
    //public Sprite[] sprites;//���� Enemy�߰��� ����� Sprite�迭

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        //Invoke("Think", 2);
    }

    /*void Update() //�� �����Ӹ��� ȣ��
    {

    }*/

    void FixedUpdate()//���� ���� ������Ʈ �ֱ�� ����ȭ => �ַ� ���� ���� ���� �۾��� ���� => �ð� ������ �����ϰ� �����Ǹ�, ������ ����Ʈ�� ������ ���� ����
    {
        if (EnemyType == 1)
        {
            //�̰ɷ� �����̴°� ��� ������Ʈ �ϸ鼭 �̵���Ŵ, Move
            rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

            //���� ���̱� �κ�, PlatForm Check
            Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);//�̰ɷ� ��ġ���� ���� ����
            //RayCast ����
            Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
            //���̿� ���� ���� ������ ����
            RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, raycastLength, LayerMask.GetMask("Default")); //Default�� �ӽ� ����
            if (rayHit.collider == null)    //�տ� Platform�̸��� Ÿ���� ����(null)
            {
                //Debug.Log("��������");
                nextMove *= -1;
                CancelInvoke();
                Invoke("Think", 2);
            }
        }
    }
    void Think()
    {
        nextMove = Random.Range(-1, 2); //�������� �ּڰ��� ����� ������, �ִ밪�� ��ǥ�� 1�� �ƴ� 2�� ������         
        //float nextThinkTime = Random.Range(2f, 5f);
        TransformAnim();
        Invoke("Think", 2); //����Լ�
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
            Debug.Log("�¾ҽ��ϴ�");
            OnDamaged(collision.transform.position);
        }
    }
    
    void OnHit(int dmg)//�������� �߻���
    {
        Enemy_Life -= dmg;

        //�浹�� ��¦ȿ��
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        Invoke("OffDamaged", 0.2f);

        if (Enemy_Life <= 0)
        {
            Destroy(gameObject);//���� ��ü �ı�
        }
    }

    void OnTriggerEnter2D(Collider2D collision)//�浹 �߻��� Enemy�� Enemy_Life ����
    {
        if (collision.gameObject.tag == "Bullet")
        {
            BulletController bullet = collision.gameObject.GetComponent<BulletController>();  //�� �κ��� �ٸ� ��ũ��Ʈ�� ���� ȣ���ؼ� �� ������ �ְ� �ϴ� �κ� �����߿�!
            OnHit(bullet.dmg);
            Destroy(collision.gameObject);//�÷��̾��� �Ѿ��� �����Ѵ� �̶�, collision���� �� �ľ�
        }
    }
    void OnDamaged(Vector2 targetPos)
    {
        //gameObject.layer = ; //���� �ǰ� ���̾�

        //�ǰݽ� ���� �����Ŵ
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //�ش� �ð� �Ŀ� OffDamaged�߻�
        Invoke("OffDamaged", 1);
    }
    void OffDamaged()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}