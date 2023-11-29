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
    [SerializeField] float spawnForce = 6f;
    [SerializeField] float itemDropDelay = 0.083f;
    [SerializeField] int spawnCount;

    private Animator anim;
    private string childObjectName = "OverheadImage";
    private int currentSpawnCount = 0;

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
        InvokeRepeating("ChestItemDrop", 0.2f, itemDropDelay);
    }

    //������ ���
    private void ChestItemDrop()
    {
        if(currentSpawnCount >= spawnCount)
        {
            CancelInvoke("ChestItemDrop");
            return;
        }

        //���� ����
        int rnd = Random.Range(0, items.Length);
        float rndForceX = Random.Range(-2f, 2f);

        Vector2 spawnPoint = transform.position;
        Vector2 randomForce = new Vector2(rndForceX, spawnForce);

        GameObject spawnItem = Instantiate(items[rnd], spawnPoint, Quaternion.identity);

        Rigidbody2D itemRigidbody = spawnItem.GetComponent<Rigidbody2D>();

        if (itemRigidbody != null)
        {
            itemRigidbody.AddForce(randomForce, ForceMode2D.Impulse);
            currentSpawnCount++;
        }
        else
            Debug.Log("Spawned Item Does Not Have A Rigidbody Component");

        Debug.Log("Item " + rnd + " Dropped!");

        //Instantiate(items[rnd, );
    }
}
