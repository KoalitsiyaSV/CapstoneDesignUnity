using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//���ڸ� ���� ��ũ��Ʈ
public class ChestFunction : MonoBehaviour
{
    /*
    ���ڴ� �߸� NPC�� �з��Ͽ��� > NPCDetection ��ũ��Ʈ���� Ž����
    ���� ������Ʈ�� OverheadImage�� Ȱ��ȭ ���θ� ���� �÷��̾ Ư�� ���� �ȿ� ���� �� ��ȣ�ۿ��� �����ϵ��� ��
    */

    [SerializeField] bool canInteract;
    [SerializeField] GameObject[] items;

    private Animator anim;
    private string childObjectName = "OverheadImage";

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Transform childTranform = transform.Find(childObjectName);

        if(childTranform != null)
        {
            canInteract = childTranform.gameObject.activeSelf;

            //��ȣ�ۿ� �����ϰ� FŰ�� �� �� ������ ������ �� ���� ����
            if (canInteract && Input.GetKeyUp(KeyCode.F))
            {
                ChestOpen(childTranform);
            }
        }
    }

    //���� ����
    private void ChestOpen(Transform childTranform)
    {
        Debug.Log("Open");
        //���� ������Ʈ �ı�
        Destroy(childTranform.gameObject);

        //�ִϸ��̼� ���
        anim.SetFloat("Speed", 1f);

        //������ ���
        ChestItemDrop();
    }

    //������ ���
    private void ChestItemDrop()
    {
        //���� ����
        int rnd = Random.Range(1, 4);

        Debug.Log("Item " + rnd + " Dropped!");

        //Instantiate(items[rnd, );
    }
}
