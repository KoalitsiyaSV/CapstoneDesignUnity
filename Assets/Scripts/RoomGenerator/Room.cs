using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Room ��ũ��Ʈ�� ������ Room�� �����ϴ� ��ũ��Ʈ
 
public class Room : MonoBehaviour {
    [SerializeField] GameObject upPortal;
    [SerializeField] GameObject downPortal;
    [SerializeField] GameObject stair1;
    [SerializeField] GameObject stair2;
    [SerializeField] GameObject leftBase;
    [SerializeField] GameObject rightBase;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject leftPortalPrefab;
    [SerializeField] GameObject rightPortalPrefab;
    [SerializeField] GameObject portalPrefab;
    public Vector2Int RoomIndex { get; set; }

    public Vector3 getUpPortalPosition {
        get { return upPortal.transform.position; } 
    }
    public Vector3 getDownPortalPosition {
        get { return downPortal.transform.position; }
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
    }
    public void OpenLRDoor(Vector2Int direction, string position, Room room) {
        string portalName = "";
        int i = Random.Range(1, 3);
        if (direction == Vector2Int.left) {
            leftBase.SetActive(false);
            if (position == "lower") {
                portalName = position + "Left" + i;
            }
            else if (position == "basic") {
                portalName = position + "Left";
            }
            else if (position == "upper") {
                portalName = position + "Left" + i;
            }
            var door = Instantiate(leftPortalPrefab, room.transform.position, Quaternion.identity);
            door.transform.Find(portalName).gameObject.SetActive(true);
            var portal = Instantiate(portalPrefab, door.transform.Find(portalName).position, Quaternion.identity);
            portal.tag = "Left";
            portal.gameObject.SetActive(true);
            door.transform.parent = room.transform;
            portal.transform.parent = room.transform;
        }
        else if (direction == Vector2Int.right) {
            rightBase.SetActive(false);
            if (position == "lower") {
                portalName = position + "Right" + i;
            }
            else if (position == "basic") {
                portalName = position + "Right";
            }
            else if (position == "upper") {
                portalName = position + "Right" + i;
            }
            var door = Instantiate(rightPortalPrefab, room.transform.position, Quaternion.identity);
            door.transform.Find(portalName).gameObject.SetActive(true);
            var portal = Instantiate(portalPrefab, door.transform.Find(portalName).position, Quaternion.identity);
            portal.tag = "Right";
            portal.gameObject.SetActive(true);
            door.transform.parent = room.transform;
            portal.transform.parent = room.transform;
        }        
    }
    // OpenDoor�޼ҵ�� ������ Room���� ������ ���� ������ �Ű������� �޾Ƽ� �ش� ���� ���� �޼ҵ�
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

