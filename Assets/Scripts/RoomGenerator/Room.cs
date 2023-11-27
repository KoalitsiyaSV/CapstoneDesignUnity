using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Room ��ũ��Ʈ�� ������ Room�� �����ϴ� ��ũ��Ʈ
 
public class Room : MonoBehaviour {
    [SerializeField] GameObject upPortal;
    [SerializeField] GameObject downPortal;
    [SerializeField] GameObject leftPortal;
    [SerializeField] GameObject rightPortal;
    [SerializeField] GameObject stair1;
    [SerializeField] GameObject stair2;
    [SerializeField] GameObject leftBase;
    [SerializeField] GameObject rightBase;

    public Vector2Int RoomIndex { get; set; }

    public Vector3 getUpPortalPosition {
        get { return upPortal.transform.position; } 
    }
    public Vector3 getRightPortalPosition(string entryPortalPosition) {
        string portalCategory = entryPortalPosition + "Right";
        string exitPortalName = entryPortalPosition + "RightPortal";
        Transform category = rightPortal.transform.Find(portalCategory);
        Vector3 exitPortalPosition = category.transform.Find(exitPortalName).position;
        return exitPortalPosition;
    }
    public Vector3 getDownPortalPosition {
        get { return downPortal.transform.position; }
    }
    public Vector3 getLeftPortalPosition(string entryPortalPosition) {
        string portalCategory = entryPortalPosition + "Left";
        string exitPortalName = entryPortalPosition + "LeftPortal";
        Transform category = leftPortal.transform.Find(portalCategory);
        Vector3 exitPortalPosition = category.transform.Find(exitPortalName).position;
        return exitPortalPosition;
    }
    // get###DoorPosition ��ũ��Ʈ�� �ش� ���� ��ġ�� Vector3������ ��ȯ

    public void OpenDoor(Vector2Int direction) {
        if (direction == Vector2Int.up) {
            upPortal.SetActive(true);
            int stair = Random.Range(1, 3);
            if (stair == 1)
                stair1.SetActive(true);
            else
                stair2.SetActive(true);
        }
        if (direction == Vector2Int.down) {
            downPortal.SetActive(true);
        }
        if (direction == Vector2Int.left) {
            leftPortal.SetActive(true);
        }
        if (direction == Vector2Int.right) {
            rightPortal.SetActive(true);
        }
    }
    //public void OpenUpperDoor(Vector2Int direction) {
    //    if (direction == Vector2Int.left) {
    //        leftPortal.transform.Find("upperLeft").gameObject.SetActive(true);            
    //    }
    //    if (direction == Vector2Int.right) {
    //        rightPortal.transform.Find("upperRight").gameObject.SetActive(true);
    //    }
    //}
    ////public void OpenLowerDoor(Vector2Int direction) {
    ////    if (direction == Vector2Int.left) {
    ////        leftPortal.transform.Find("lowerLeft").gameObject.SetActive(true);
    ////    }
    ////    if (direction == Vector2Int.right) {
    ////        rightPortal.transform.Find("lowerRight").gameObject.SetActive(true);
    ////    }
    ////}
    public void OpenLRDoor(Vector2Int direction, string position) {
        if(position == "lower") {
            if (direction == Vector2Int.left) {
                leftBase.SetActive(false);
                leftPortal.transform.Find("lowerLeft").gameObject.SetActive(true);
            }
            if (direction == Vector2Int.right) {
                rightBase.SetActive(false);
                rightPortal.transform.Find("lowerRight").gameObject.SetActive(true);
            }
        }
        else if(position == "upper") {
            if (direction == Vector2Int.left) {
                leftBase.SetActive(false);
                leftPortal.transform.Find("upperLeft").gameObject.SetActive(true);
            }
            if (direction == Vector2Int.right) {
                rightBase.SetActive(false);
                rightPortal.transform.Find("upperRight").gameObject.SetActive(true);
            }
        }
        else if(position == "basic") {
            if (direction == Vector2Int.left) {
                leftBase.SetActive(false);
                leftPortal.transform.Find("basicLeft").gameObject.SetActive(true);
            }
            if (direction == Vector2Int.right) {
                rightBase.SetActive(false);
                rightPortal.transform.Find("basicRight").gameObject.SetActive(true);
            }
        }
        
    }
    // OpenDoor�޼ҵ�� ������ Room���� ������ ���� ������ �Ű������� �޾Ƽ� �ش� ���� ���� �޼ҵ�
}

