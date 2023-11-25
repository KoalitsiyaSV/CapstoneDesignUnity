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

    public Vector2Int RoomIndex { get; set; }

    public Vector3 getUpPortalPosition {
        get { return upPortal.transform.position; } 
    }
    public Vector3 getRightPortalPosition {
        get { return rightPortal.transform.position; }
    }
    public Vector3 getDownPortalPosition {
        get { return downPortal.transform.position; }
    }
    public Vector3 getLeftPortalPosition {
        get { return leftPortal.transform.position; }
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
    // OpenDoor메소드는 생성된 Room에서 생성할 문의 방향을 매개변수로 받아서 해당 문을 여는 메소드
}

