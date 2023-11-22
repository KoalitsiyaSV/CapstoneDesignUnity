using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class BossController : MonoBehaviour
{
    Animator bossAnimator;
    SpriteRenderer bossSpriteRenderer;

    public Transform target;
    public float speed = 0.1f;
    public float range = 10f;

    private bool isAttack = false;

    // Start is called before the first frame update
    void Awake()
    {
        bossAnimator = GetComponent<Animator>();
        bossSpriteRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating("BossPattern", 0f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        float dis = Vector3.Distance(transform.position, target.position); //내위치와 target의 위치 사이의 거리를 구함

        if (dis <= 30 && dis >= 5 && !isAttack) // 거리가 10칸 안으로 좁혀졌으면 쫒기 시작
        {
            Move();
        }
        else return;
    }

    void Move()
    {
        float dir = target.position.x - transform.position.x; //2d이기에 좌우만 빼면됨 (내x위치 - targetx위치)
        dir = (dir > 0) ? -1 : 1; //방향조절 dir의 x거리가 -라면 -1,아니면 1
        TransformTurnAnim(dir);
        transform.Translate(new Vector2(dir, 0) * speed * Time.deltaTime * 1/2);
    }

    void BossPattern()
    {
        int nextPattern = Random.Range(0, 4);
        bossAnimator.SetTrigger("isAttack");
        bossAnimator.SetInteger("AttackType", nextPattern);
        isAttack = true;
        Invoke("Reverse", 2f);
    }

    void Reverse()
    {
        isAttack = false;
    }

    void TransformTurnAnim(float direction)
    {
        if (direction == 1)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (direction == -1)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }
}