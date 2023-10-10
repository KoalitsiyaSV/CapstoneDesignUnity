using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public int dmg;
    public float DestroyTime;//��Ÿ�(�ð� ������ ������)

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
        //���� �ð�(DestroyTime)�� ������ ���� ���� �޼ҵ� ȣ��
        Invoke("DestroyObj", DestroyTime);
    }

    void DestroyObj()
    {
        Destroy(gameObject);
    }
}
