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
    // OpenDoor�޼ҵ�� ������ Room���� ������ ���� ������ �Ű������� �޾Ƽ� �ش� ���� ���� �޼ҵ�
}

