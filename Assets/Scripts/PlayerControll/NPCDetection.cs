using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NPC를 탐지하는 스크립트
public class NPCDetection : MonoBehaviour
{
    /*
    PS) OverheadImage는 중립 NPC의 하위 오브젝트로 플레이어의 중립 NPC 탐지 범위 안에 중립 NPC가 확인될 시,
          해당 NPC의 위쪽에 나타나는 상호작용 키를 알려주는 이미지
    */

    //현재 범위 안에 둘이상의 중립 NPC가 존재할 때의 처리는 없음
    //private Queue<Transform> detectedNPCs = new Queue<Transform>();
    private Transform overheadImage;
    private string targetString = "OverheadImage";
    //private int targetColliderIndex = 3;

    void Start()
    {

    }

    private void Update()
    {

    }

    //탐지 범위 안에 NPC가 확인되었을 시
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("NPC"))
        {
            Debug.Log("Detected");

            //해당 NPC의 Transform과 OverheadImage라는 이름의 하위 오브젝트를 받아옴
            //detectedNPCs.Enqueue(other.transform);
            Transform targetTransform = other.transform;
            //overheadImage = detectedNPCs.Peek().Find(targetString);
            overheadImage = targetTransform.Find(targetString);

            //overheadImage라는 이름의 하위 오브젝트가 NULL이 아닐 시 활성화
            if (overheadImage != null)
                overheadImage.gameObject.SetActive(true);
        }
    }

    //확인되었던 NPC가 탐지 범위를 벗어날 시
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            //if (overheadImage == null)
            //    Debug.Log("Disappear");

            //OverheadImage라는 이름의 하위 오브젝트가 NULL이 아닐 시 비활성화
            if (overheadImage != null)
            {
                overheadImage.gameObject.SetActive(false);
                //detectedNPCs.Dequeue();
            }
        }
    }
}