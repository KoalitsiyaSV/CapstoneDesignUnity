using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//NPC�� Ž���ϴ� ��ũ��Ʈ
public class NPCDetection : MonoBehaviour
{
    /*
    PS) OverheadImage�� �߸� NPC�� ���� ������Ʈ�� �÷��̾��� �߸� NPC Ž�� ���� �ȿ� �߸� NPC�� Ȯ�ε� ��,
          �ش� NPC�� ���ʿ� ��Ÿ���� ��ȣ�ۿ� Ű�� �˷��ִ� �̹���
    */

    //���� ���� �ȿ� ���̻��� �߸� NPC�� ������ ���� ó���� ����
    private GameObject targetObject;
    private Transform overheadImage;
    private string targetTag;
    private string targetString = "OverheadImage";

    //Ž�� ���� �ȿ� NPC�� Ȯ�εǾ��� ��
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("NPC"))
        {
            Debug.Log("Detected");

            //�ش� NPC�� Transform�� OverheadImage��� �̸��� ���� ������Ʈ�� �޾ƿ�
            Transform targetTransform = other.transform;

            targetObject = targetTransform.gameObject;
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
            Debug.Log("Disappear");
                 
            //OverheadImage��� �̸��� ���� ������Ʈ�� NULL�� �ƴ� �� ��Ȱ��ȭ
            if (overheadImage != null)
            {
                overheadImage.gameObject.SetActive(false);
            }

            if (targetObject != null)
            {
                targetObject = null;
            }
        }
    }
    public GameObject GetTargetObject()
    {
        return targetObject;
    }
}