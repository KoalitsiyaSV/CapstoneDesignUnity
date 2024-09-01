using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChestInterationDetector : PlayerDetector
{
    [Header("Chest Data")]
    [SerializeField] GameObject[] items;
    private Collider2D chestCollider;

    [Header("Spawn Effect")]
    [SerializeField] float spawnForce = 6f;
    [SerializeField] float itemDropDelay = 0.083f;
    [SerializeField] int spawnCount;

    private Animator anim;
    private int CurrentSpawnCount = 0;

    protected override void Start()
    {
        base.Start();

        anim = GetComponent<Animator>();
        chestCollider = GetComponent<Collider2D>();
    }

    //targetObject�� �ִٸ�, FŰ�� ������ ���� ����
    private void Update()
    {
        //�÷��̾ Ž���Ǿ� �ְ� FŰ�� ������ ��
        if(player != null && Input.GetKeyDown(KeyCode.F))
        {
            //overheadImage�� �ִٸ� ���� ������ ���� ����
            if (overheadImage != null)
                ChestOpen();
            else
                Debug.Log("Chest Already Opened");
        }
    }

    //���� ����
    private void ChestOpen()
    {
        Debug.Log("Chest Open");

        //�� �̻� ��ȣ�ۿ� ��
        Destroy(overheadImage.gameObject);
        chestCollider.enabled = false;

        anim.SetFloat("Speed", 1f);

        InvokeRepeating("ItemDrop", 0.2f, itemDropDelay);
    }

    //������ ���
    private void ItemDrop()
    {
        //�ݺ�Ƚ���� ���� �� �ݺ� ����
        if(CurrentSpawnCount >= spawnCount)
        {
            CancelInvoke("ItemDrop");
            return;
        }

        float rndForceHorizontal = Random.Range(-2f, 2f);
        
        Vector2 spawnPoint = transform.position;
        Vector2 randomForce = new Vector2(rndForceHorizontal, spawnForce);

        //������ �������� �����ϰ� �������� ������ ���� ���� ���ϰ� ������ ����ŭ �Ʒ����� ���� ���� ����
        GameObject spawnItem = Instantiate(ItemDatabase.instance.fieldItemPrefab, spawnPoint, Quaternion.identity);
        Rigidbody2D itemRigidbody = spawnItem.GetComponent<Rigidbody2D>();
        spawnItem.GetComponent<FieldItem>().SetItem(ItemDatabase.instance.itemDB[Random.Range(0, 5)]);

        if (itemRigidbody != null)
        {
            itemRigidbody.AddForce(randomForce, ForceMode2D.Impulse);
            CurrentSpawnCount++;
        }
        else
            Debug.Log("Spawned Item Does Not Have A Rigidbody Component");
    }
}