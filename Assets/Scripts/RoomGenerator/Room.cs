using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Room 스크립트는 생성된 Room을 제어하는 스크립트
 
public class Room : MonoBehaviour {
    [SerializeField] GameObject upPortal;
    [SerializeField] GameObject downPortal;
    [SerializeField] GameObject leftPortal;
    [SerializeField] GameObject rightPortal;
    [SerializeField] GameObject stair1;
    [SerializeField] GameObject stair2;
    [SerializeField] GameObject leftBase;
    [SerializeField] GameObject rightBase;
    [SerializeField] GameObject spawnPoint;

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
    // get###DoorPosition 스크립트는 해당 문의 위치를 Vector3값으로 반환

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
    // OpenDoor메소드는 생성된 Room에서 생성할 문의 방향을 매개변수로 받아서 해당 문을 여는 메소드
    public void SpawnSpawnPoint() {
        int numOfSpawnPoint = Random.Range(2, 6);
        int spawnCount = 0;
        Transform spawn = spawnPoint.transform;
        for ( int n = 0; n < spawn.childCount; n++) {
            int i = Random.Range(0, 2);
            if (spawnCount < numOfSpawnPoint) {
                if (i == 1) {
                    spawn.GetChild(n).gameObject.SetActive(true); 
                    numOfSpawnPoint++;
                }
            }
        }
    }
}

