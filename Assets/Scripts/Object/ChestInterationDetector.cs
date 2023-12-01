using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChestInterationDetector : PlayerDetector
{
    [Header("Chest Data")]
    [SerializeField] GameObject[] items;

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
    }

    //targetObject가 있다면, F키를 누르면 상자 열림
    private void Update()
    {
        //플레이어가 탐지되어 있고 F키를 눌렀을 떄
        if(player != null && Input.GetKeyDown(KeyCode.F))
        {
            //overheadImage가 있다면 아직 열리지 않은 상자
            if (overheadImage != null)
                ChestOpen();
            else
                Debug.Log("Chest Already Opened");
        }
    }

    //상자 열기
    private void ChestOpen()
    {
        Debug.Log("Chest Open");

        //더 이상 상호작용 불
        Destroy(overheadImage.gameObject);

        anim.SetFloat("Speed", 1f);

        InvokeRepeating("ItemDrop", 0.2f, itemDropDelay);
    }

    //아이템 드롭
    private void ItemDrop()
    {
        //반복횟수에 도달 시 반복 중지
        if(CurrentSpawnCount >= spawnCount)
        {
            CancelInvoke("ItemDrop");
            return;
        }

        int rnd = Random.Range(0, items.Length);
        float rndForceHorizontal = Random.Range(-2f, 2f);
        
        Vector2 spawnPoint = transform.position;
        Vector2 randomForce = new Vector2(rndForceHorizontal, spawnForce);

        //랜덤한 아이템을 스폰하고 수평으로 랜덤한 값의 힘을 가하고 정해진 값만큼 아래에서 위로 힘을 가함
        GameObject spawnItem = Instantiate(items[rnd], spawnPoint, Quaternion.identity);
        Rigidbody2D itemRigidbody = spawnItem.GetComponent<Rigidbody2D>();

        if (itemRigidbody != null)
        {
            itemRigidbody.AddForce(randomForce, ForceMode2D.Impulse);
            CurrentSpawnCount++;
        }
        else
            Debug.Log("Spawned Item Does Not Have A Rigidbody Component");

        Debug.Log("Item " + rnd + " Dropped!");
    }
}