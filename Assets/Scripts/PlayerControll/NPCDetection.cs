using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NPC�� Ž���ϴ� ��ũ��Ʈ
public class NPCDetection : MonoBehaviour
{
    /*
    PS) OverheadImage�� �߸� NPC�� ���� ������Ʈ�� �÷��̾��� �߸� NPC Ž�� ���� �ȿ� �߸� NPC�� Ȯ�ε� ��,
          �ش� NPC�� ���ʿ� ��Ÿ���� ��ȣ�ۿ� Ű�� �˷��ִ� �̹���
    */

    //���� ���� �ȿ� ���̻��� �߸� NPC�� ������ ���� ó���� ����
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

    //Ž�� ���� �ȿ� NPC�� Ȯ�εǾ��� ��
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("NPC"))
        {
            Debug.Log("Detected");

            //�ش� NPC�� Transform�� OverheadImage��� �̸��� ���� ������Ʈ�� �޾ƿ�
            //detectedNPCs.Enqueue(other.transform);
            Transform targetTransform = other.transform;
            //overheadImage = detectedNPCs.Peek().Find(targetString);
            overheadImage = targetTransform.Find(targetString);

            //overheadImage��� �̸��� ���� ������Ʈ�� NULL�� �ƴ� �� Ȱ��ȭ
            if (overheadImage != null)
                overheadImage.gameObject.SetActive(true);
        }
    }

    //Ȯ�εǾ��� NPC�� Ž�� ������ ��� ��
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            //if (overheadImage == null)
            //    Debug.Log("Disappear");

            //OverheadImage��� �̸��� ���� ������Ʈ�� NULL�� �ƴ� �� ��Ȱ��ȭ
            if (overheadImage != null)
            {
                overheadImage.gameObject.SetActive(false);
                //detectedNPCs.Dequeue();
            }
        }
    }
}