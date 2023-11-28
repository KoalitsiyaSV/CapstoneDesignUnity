using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//상자를 여는 스크립트
public class ChestFunction : MonoBehaviour
{
    /*
    상자는 중립 NPC로 분류하였음 > NPCDetection 스크립트에서 탐지됨
    하위 오브젝트인 OverheadImage의 활성화 여부를 통해 플레이어가 특정 범위 안에 있을 때 상호작용이 가능하도록 함
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

            //상호작용 가능하고 F키가 한 번 눌렀다 떼졌을 때 상자 열기
            if (canInteract && Input.GetKeyUp(KeyCode.F))
            {
                ChestOpen(childTranform);
            }
        }
    }

    //상자 열기
    private void ChestOpen(Transform childTranform)
    {
        Debug.Log("Open");
        //하위 오브젝트 파괴
        Destroy(childTranform.gameObject);

        //애니메이션 재생
        anim.SetFloat("Speed", 1f);

        //아이템 드롭
        ChestItemDrop();
    }

    //아이템 드롭
    private void ChestItemDrop()
    {
        //랜덤 변수
        int rnd = Random.Range(1, 4);

        Debug.Log("Item " + rnd + " Dropped!");

        //Instantiate(items[rnd, );
    }
}
