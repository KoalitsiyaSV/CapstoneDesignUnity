using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using Unity.VisualScripting;

//using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator anim;
    Rigidbody2D rigid;

    [Header("Head")]
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
    //test1203
    public bool E_follow= false;
    public bool E_attacked = false;
    public bool Is_Attacked = false;
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
        TransformAnim();
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
        /*if (nextMove > 0)
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
        }*/
        

        if (E_follow)
        {
            anim.SetBool("Enemy_Walk", true);
        }
        else
        {
            anim.SetBool("Enemy_Walk", false);
        }

        if (Is_Attacked == true)
        {
            if(anim.GetBool("Enemy_Attack_1")==false && anim.GetBool("Enemy_Attack_2") == false)
            {
                anim.SetBool("Enemy_Walk", true);
                Is_Attacked = false;
            }
        }
        /*
        if (anim.GetBool("Enemy_Attack_1") == false && anim.GetBool("Enemy_Attack_2") == false && E_attacked == false)
        {
            Debug.Log("action");
            anim.SetBool("Enemy_Walk", false);
        } 
         */

        //if (anim.GetBool("Enemy_Attack_1") && anim.GetBool("Enemy_Attack_2") == false)
        //{
        //    anim.SetBool("Enemy_walk", false);
        //}

        //if (Mathf.Abs(rigid.velocity.x) < 0.2f)
        //    anim.SetBool("Enemy_walk", false);
        //else
        //    anim.SetBool("Enemy_walk", true);
    }
    private void anim_Attack()//해당하는 메소드는 에니메이터에서 이벤트를 통해서 호출하는중
    {
        if (E_attacked && anim.GetBool("Enemy_Attack_1"))
        {
            
            E_attacked = false;
            anim.SetBool("Enemy_Attack_1", false);
            anim.SetBool("Enemy_Attack_2", false);
            anim.SetBool("Enemy_Walk", true);
            Debug.Log("action");
        }
        else if (E_attacked && anim.GetBool("Enemy_Attack_2"))
        {
            E_attacked = false;
            anim.SetBool("Enemy_Attack_1", false);
            anim.SetBool("Enemy_Attack_2", false);
            anim.SetBool("Enemy_Walk", true);
            Debug.Log("action");
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
        //dmaged
        enemy_Life -= dmg;
        //gameObject.layer = ; //���� �ǰ� ���̾�

        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        Invoke("OffDamaged", 1);
        //anim.SetBool("Enemy_Damaged", true);//만약일반 몬스터라면 이거 필요, 보스몹에 지금은 없음
    }
    void OffDamaged()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
        //anim.SetBool("Enemy_Damaged", false);//만약일반 몬스터라면 이거 필요, 보스몹에 지금은 없음
    }

    void Enemy_Attack(Vector2 direction)
    {
        //Enemy is detect Player
        Vector2 rayStartPos = new Vector2(rigid.position.x - attackRayLength/2, rigid.position.y-0.4f);
        Debug.DrawRay(rayStartPos, Vector3.right * attackRayLength, Color.red);
        
        RaycastHit2D rayHit_attack = Physics2D.Raycast(rayStartPos, Vector3.right, attackRayLength, LayerMask.GetMask("Player")); //Layer�� Player�� ������Ʈ Ž��
        
        if (rayHit_attack.collider != null)
        {

            int nextAttack = Random.Range(1, 3);
            if (Vector2.Distance(transform.position, rayHit_attack.collider.transform.position) < enemy_AttackDistance)
            {
                E_follow = false;//해당하는 범위에 들어왔으니 Idle
                //해당하는 플레이어에 따라 방향 변경
                if ((transform.position.x - rayHit_attack.collider.transform.position.x)> 0)
                {
                    transform.localScale = new Vector2(1, 1);
                }
                else
                {
                    transform.localScale = new Vector2(-1, 1);
                }
                
                if(ememy_Type == 2)//shot bullet
                {
                    anim.SetBool("Enemy_Attack", true);
                    Fire_Enemy(direction);
                }

                if (ememy_Type == 3)//보스몹 근접공격
                {
                    if (enemy_Attack_Delay <= max_Attack_Delay)
                    {
                        return;
                    }
                    else
                    {
                        E_attacked = true;
                        //Debug.Log("ee");
                        if (nextAttack == 1)
                        {
                            anim.SetBool("Enemy_Attack_2", true);
                            enemy_Attack_Delay = 0;
                        }
                        else
                        {
                            anim.SetBool("Enemy_Attack_1", true);
                            enemy_Attack_Delay = 0;
                        }
                    }
                    //anim_Attack();
                }

            }
            else//만약 공격 사거리 내부에 적이 없다면, 따라간다.
            {
                E_follow = true;//해당하는 범위로 가기위해 walk
                //Enemy follow Player
                if (!E_attacked)//만약공격을 하고 있다면 멈취야 하기 때문에, 공격중이 아닐 경우에만 움직이게 함
                {
                    Vector3 targetPos = new Vector3(rayHit_attack.collider.transform.position.x, transform.position.y, transform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * enemy_Move_Speed);
                }
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
        //anim.SetBool("Enemy_Attack", false);
    }
    void Enemy_Relode()
    {
        enemy_Attack_Delay += Time.deltaTime;
    }
}