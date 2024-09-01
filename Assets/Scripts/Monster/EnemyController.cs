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
    protected SpriteRenderer enemySpriteRenderer;
    protected Animator enemyAnimator;
    protected Rigidbody2D enemyRigidbody;
    protected Collider2D[] enemyColliderComponents;

    [Header("Head")]
    public int nextMove;    //�ٽ� �̵��ϴµ� �ɸ��� �ð�
    public float enemy_Life;
    public float raycastLength;

    public float attackRayLength;
    public float enemy_Move_Speed;
    public int enemy_Attack_dmg;
    //Long Distance Attack
    //public float enemy_Distance;
    [Header("Enemy Type")]
    [SerializeField] protected int enemyType; //0 = Patrol, 1 = Sleep

    [Header("Enemy Status")]
    public float enemyAttackRange;//Enemy ���ݻ�Ÿ�
    //public float max_Attack_Delay;    //
    public GameObject bullet_E;         
    public float bullet_E_Speed;
    public Transform bullet_Pos;
    [SerializeField] protected float enemySightStartPoint;

    [Header("Enemy Action Control")]
    [SerializeField] protected bool enemyDirection = false;//true = 오른쪽을 바라봄, false = 왼쪽을 바라봄
    [SerializeField] protected Transform targetObj;
    [SerializeField] protected bool isAction = false;
    [SerializeField] protected bool canAttack = true;
    protected int enemyPatternCount;
    protected float enemyAttackCooldown;
    protected float enemyAttackCoolTime;

    //[Header("Patrol Test")]
    //[SerializeField] float enemyPatrolWaitTime;
    //[SerializeField] float enemyPatrolTime;
    //protected Transform patrolSpot;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        enemyAnimator = GetComponent<Animator>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyColliderComponents = GetComponents<Collider2D>();

        //enemyPatrolTime = enemyPatrolWaitTime;
        //patrolSpot.position = new Vector2(Random.Range(-5,5), transform.position.y);
    }

    //void Update()
    //{
    //
    //}

    protected virtual void FixedUpdate()
    {
        Destroy_Enemy();

        Enemy_Relode();

        AfterPlayerDetect();
        //TransformAnim();

        //Vector2 EnemyDirection = Vector2.left;//찿았을때에 해당하는 방향으로 총알 발사시에 필요함
        //if (transform.localScale.x < 0)
        //{
        //    EnemyDirection = Vector2.right;
        //}
        //else if (transform.localScale.x > 0)
        //{
        //    EnemyDirection = Vector2.left;
        //}



        //if (targetObj == null)
        //{
        //    if (enemyType == 0)
        //    {
        //        Patrol();
        //    }
        //}
        //else
        //    AfterPlayerDetect();

        //if (enemyType == 1)
        //{
        //    //�̰ɷ� �����̴°� ��� ������Ʈ �ϸ鼭 �̵���Ŵ, Move
        //    enemyRigidbody.velocity = new Vector2(nextMove, enemyRigidbody.velocity.y);

        //    //���� ���̱� �κ�, PlatForm Check
        //    Vector2 frontVec = new Vector2(enemyRigidbody.position.x + nextMove * 0.3f, enemyRigidbody.position.y);//�̰ɷ� ��ġ���� ���� ����
        //    //RayCast ����
        //    Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        //    //���̿� ���� ���� ������ ����
        //    RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, raycastLength, LayerMask.GetMask("Default")); //Default�� �ӽ� ����
        //    if (rayHit.collider == null)    //�տ� Platform�̸��� Ÿ���� ����(null)
        //    {
        //        //Debug.Log("��������");
        //        nextMove *= -1;
        //        CancelInvoke();
        //        Invoke("Think", 2);
        //    }
        //}
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2); //�������� �ּڰ��� ����� ������, �ִ밪�� ��ǥ�� 1�� �ƴ� 2�� ������         
        //float nextThinkTime = Random.Range(2f, 5f);
        //TransformAnim();
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
        enemySpriteRenderer.color = new Color(1, 1, 1, 0.4f);
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
        //if (enemyColliderComponents[1].isActiveAndEnabled && collision.gameObject.CompareTag("Player"))
        //{
        //    Debug.Log("Player Hit");
        //    GameManager.Instance.PlayerTakeDamage(10);
        //}
    }
    
    public void OnDamaged(int dmg)
    {
        //dmaged
        enemy_Life -= dmg;
        //gameObject.layer = ; //���� �ǰ� ���̾�

        enemySpriteRenderer.color = new Color(1, 1, 1, 0.4f);
        Invoke("OffDamaged", 1);
        //anim.SetBool("Enemy_Damaged", true);//만약일반 몬스터라면 이거 필요, 보스몹에 지금은 없음
    }
    void OffDamaged()
    {
        enemySpriteRenderer.color = new Color(1, 1, 1, 1);
        //anim.SetBool("Enemy_Damaged", false);//만약일반 몬스터라면 이거 필요, 보스몹에 지금은 없음
    }

    protected virtual void AfterPlayerDetect()
    {
        //Enemy is detect Player
        if (targetObj == null)
        {
            SightRange();
        }

        if (targetObj != null)
        {
            if (!isAction)
            {
                EnemyDirectionChange();
            }

            if (Vector2.Distance(transform.position, targetObj.position) < enemyAttackRange)
            {
                if(!isAction)
                    StartAttackAnim();

                enemyAnimator.SetBool("Enemy_Walk", false);
                ////if(ememy_Type == 2)//shot bullet
                ////{
                ////    anim.SetBool("Enemy_Attack", true);
                ////    Fire_Enemy(direction);
                ////}

                ////if (ememy_Type == 3)//보스몹 근접공격
                ////{
                ////    if (enemy_Attack_Delay <= max_Attack_Delay)
                ////    {
                ////        return;
                ////    }
                ////    else
                ////    {
                ////        E_attacked = true;
                ////        //Debug.Log("ee");
                ////        if (nextAttack == 1)
                ////        {
                ////            anim.SetBool("Enemy_Attack_2", true);
                ////            enemy_Attack_Delay = 0;
                ////        }
                ////        else
                ////        {
                ////            anim.SetBool("Enemy_Attack_1", true);
                ////            enemy_Attack_Delay = 0;
                ////        }
                ////    }
                //    //anim_Attack();
                //}
            }
            else//만약 공격 사거리 내부에 적이 없다면, 따라간다.
            {
                //E_follow = true;//해당하는 범위로 가기위해 walk
                if (!isAction)//만약공격을 하고 있다면 멈취야 하기 때문에, 공격중이 아닐 경우에만 움직이게 함
                {
                    MoveToPlayer();
                }
            }
        }
    }

    protected virtual void MoveToPlayer()
    {
        enemyAnimator.SetBool("Enemy_Walk", true);
        Vector3 targetPos = new Vector3(targetObj.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * enemy_Move_Speed);
    }

    //private void Patrol()
    //{
    //    if (targetObj != null)
    //        return;

    //    transform.position = Vector2.MoveTowards(transform.position, patrolSpot.position, enemy_Move_Speed * Time.deltaTime);

    //    if(Vector2.Distance(transform.position, patrolSpot.position) < 0.2)
    //    {
    //        if(enemyPatrolTime <= 0)
    //        {
    //            patrolSpot.position = new Vector2(Random.Range(-5, 5), transform.position.y);
    //            enemyPatrolTime = enemyPatrolWaitTime;
    //        }
    //        else
    //        {
    //            enemyPatrolTime -= Time.deltaTime;
    //        }
    //    }

    //    SightRange();
    //}

    //private void SightRange()
    //{
    //    Vector2 rayStartPos = new Vector2(enemySightStartPoint, enemyRigidbody.position.y - 0.4f);
    //    Debug.DrawRay(rayStartPos, Vector3.left * attackRayLength, Color.red);

    //    RaycastHit2D rayHit_attack = Physics2D.Raycast(rayStartPos, Vector3.left, attackRayLength, LayerMask.GetMask("Player"));
    //    targetObj = rayHit_attack.transform;
    //}

    //적 시야 범위
    protected void SightRange()
    {
        if (!enemyDirection)
        {
            Vector2 rayStartPos = new Vector2(enemyRigidbody.position.x + attackRayLength/ 2, enemyRigidbody.position.y - 0.4f);
            Debug.DrawRay(rayStartPos, Vector3.left * attackRayLength, Color.red);

            RaycastHit2D rayHit_attack = Physics2D.Raycast(rayStartPos, Vector3.left, attackRayLength, LayerMask.GetMask("Player"));
            targetObj = rayHit_attack.transform;
        }
        else
        {
            Vector2 rayStartPos = new Vector2(enemyRigidbody.position.x - attackRayLength / 2, enemyRigidbody.position.y - 0.4f);
            Debug.DrawRay(rayStartPos, Vector3.right * attackRayLength, Color.red);

            RaycastHit2D rayHit_attack = Physics2D.Raycast(rayStartPos, Vector3.right, attackRayLength, LayerMask.GetMask("Player"));
            targetObj = rayHit_attack.transform;
        }
    }

    protected virtual void EnemyDirectionChange()
    {
        //true = 오른쪽을 바라봄, false = 왼쪽을 바라봄
        if (transform.position.x > targetObj.position.x)
        {
            transform.localScale = new Vector2(1, 1);
            enemyDirection = true;
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
            enemyDirection = false;
        }
    }

    protected virtual void StartAttackAnim()
    {
        int rnd = Random.Range(1, enemyPatternCount + 1);

        enemyAnimator.SetInteger("EnemyAttack", rnd);

        StartCoroutine(EnemyAttackCoolDown(enemyAttackCooldown));
    }

    protected virtual void EndAttackAnim()
    {
        enemyAnimator.SetInteger("EnemyAttack", 0);
    }

    //protected virtual void StartAttackAnim()
    //{
    //    Debug.Log("Attack Start");
    //    enemyAnimator.SetBool("Enemy_Attack_1", true);

    //    StartCoroutine(EnemyAttackCoolDown(5f));
    //}

    //protected virtual void EndAttackAnim()
    //{
    //    Debug.Log("Attack End");
    //    enemyAnimator.SetBool("Enemy_Attack_1", false);
    //}

    protected virtual IEnumerator EnemyAttackCoolDown(float attackCooldown)
    {
        Debug.Log("Attack!");
        isAction = true;
        yield return new WaitForSeconds(attackCooldown);

        isAction = false;
    }

    //Enemy Long Range Attack
    void Fire_Enemy()
    {
        /*if (enemyAttackCoolTime < max_Attack_Delay)//재장전 중에는 발사하지 않는 부분, 메소드를 에니메이션에서 호출만 할 경우, 필요가 없음
            return;*/
        Vector2 direction;
        Bullet_Enemy bullet_Enemy = Instantiate(bullet_E, bullet_Pos.position, transform.rotation).GetComponent<Bullet_Enemy>();
        Rigidbody2D rigid_B = bullet_Enemy.GetComponent<Rigidbody2D>();
        SpriteRenderer sprite_B = bullet_Enemy.GetComponent<SpriteRenderer>();

        //총알의 방향을 결저한다.
        if (enemyDirection)//direction == Vector2.left
        {
            sprite_B.flipX = true;
            direction = new Vector2(-1,0);
        }
        else
        {
            sprite_B.flipX = false;
            direction = new Vector2(1, 0);
        }
        bullet_Enemy.eb_dmg = enemy_Attack_dmg;
        rigid_B.velocity = direction * bullet_E_Speed;
        //enemyAttackCoolTime = 0;//쿨타임을 초기화 하는 부분, 메소드를 에니메이션에서 호출만 할 경우, 필요가 없음
    }
    void Enemy_Relode()
    {
        enemyAttackCoolTime += Time.deltaTime;
    }
}