using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public int dmg;
    public float DestroyTime;//사거리(시간 지나면 없어짐)

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            DestroyObj();
        }
    }
    void Update()
    {

    }
    void Start()
    {
        //일정 시간(DestroyTime)이 지나면 본인 삭제 메소드 호출
        Invoke("DestroyObj", DestroyTime);
    }

    void DestroyObj()
    {
        Destroy(gameObject);
    }
}
