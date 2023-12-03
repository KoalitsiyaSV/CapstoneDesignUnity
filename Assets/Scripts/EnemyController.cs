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

    [Header("����")]
    public int ememy_Type;
    public int nextMove;    //�ٽ� �̵��ϴµ� �ɸ��� �ð�
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
    public float enemy_AttackDistance;//Enemy ���ݻ�Ÿ�
    public float enemy_Attack_Delay;  //���� ������
    public float max_Attack_Delay;    //���� �ְ� ���� ������
    public GameObject bullet_E;         
    public float bullet_E_Speed;
    public Transform bullet_Pos;
    
    //public Sprite[] sprites;//���� Enemy�߰��� ����� Sprite�迭

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        //Invoke("Think", 2);//���� ���ʹ� Ÿ�Կ� ���� �ٸ��� �Ұ���
    }

    //void Update() //�� �����Ӹ��� ȣ��
    //{
    //   
    //}

    void FixedUpdate()//���� ���� ������Ʈ �ֱ�� ����ȭ => �ַ� ���� ���� ���� �۾��� ���� => �ð� ������ �����ϰ� �����Ǹ�, ������ ����Ʈ�� ������ ���� ����
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
            //Debug.Log("Player is Detected");
            //OnDamaged(collision.transform.position);
            //OnDamaged();
        }
    }
    
    void OnHit(int dmg)//�������� �߻���
    {
        enemy_Life -= dmg;

        //�浹�� ��¦ȿ��
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        Invoke("OffDamaged", 0.2f);
    }

    void Destroy_Enemy()
    {
        if (enemy_Life <= 0)
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
            Destroy(collision.gameObject);//�÷��̾��� �Ѿ��� �����Ѵ�
        }
    }
    
    public void OnDamaged(int dmg)
    {
        //dmg��ŭ ü�°���
        enemy_Life -= dmg;
        //gameObject.layer = ; //���� �ǰ� ���̾�

        //�ǰݽ� ���� �����Ŵ
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //�ش� �ð� �Ŀ� OffDamaged�߻�
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
        RaycastHit2D rayHit_attack = Physics2D.Raycast(rayStartPos, Vector3.right, attackRayLength, LayerMask.GetMask("Player")); //Layer�� Player�� ������Ʈ Ž��
        if (rayHit_attack.collider != null)
        {
            if (Vector2.Distance(transform.position, rayHit_attack.collider.transform.position) < enemy_AttackDistance)
            {
                //�÷��̾��� ��ġ�� �� ��ǥ�� ���� ���� ����
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
    //Enemy�� ���Ÿ� ���� �߻�
    void Fire_Enemy(Vector2 direction) {
        if (enemy_Attack_Delay < max_Attack_Delay)
            return;

        Bullet_Enemy bullet_Enemy = Instantiate(bullet_E, bullet_Pos.position, transform.rotation).GetComponent<Bullet_Enemy>();
        Rigidbody2D rigid_B = bullet_Enemy.GetComponent<Rigidbody2D>();
        SpriteRenderer sprite_B = bullet_Enemy.GetComponent<SpriteRenderer>();

        //�Ѿ���SpriteRenderer�� ���� ���̴� �̹����� �״�� Ȥ�� �����Ͽ� ȭ���� ���ư��°��� ǥ���մϴ�. 
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