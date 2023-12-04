using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Room 스크립트는 생성된 Room을 제어하는 스크립트

public class Room : MonoBehaviour {
    [SerializeField] GameObject portalPrefab;

    public Vector2Int RoomIndex { get; set; }
    private void Start() {

    }
    private void Update() {
        if (this.isActiveAndEnabled) {
            if (isEnemyInRoom()) {
                portalDeAactive();
            }
            else {
                portalActive();
            }
        }
    }
    public void OpenDoor(Vector2Int direction, Room room) {
        int i = Random.Range(1, 3);
        if (direction == Vector2Int.left) {
            var portal = Instantiate(portalPrefab, this.transform.Find("PortalPoints").transform.Find("Left").position, Quaternion.identity);
            portal.tag = "Left";
            portal.gameObject.SetActive(true);
            portal.transform.parent = room.transform;
        }
        else if (direction == Vector2Int.right) {
            var portal = Instantiate(portalPrefab, this.transform.Find("PortalPoints").transform.Find("Right").position, Quaternion.identity);
            portal.tag = "Right";
            portal.gameObject.SetActive(true);
            portal.transform.parent = room.transform;
        }
        else if (direction == Vector2Int.up) {
            var portal = Instantiate(portalPrefab, this.transform.Find("PortalPoints").transform.Find("Up").position, Quaternion.identity);
            portal.tag = "Up";
            portal.gameObject.SetActive(true);
            portal.transform.parent = room.transform;
        }
        else if (direction == Vector2Int.down) {
            var portal = Instantiate(portalPrefab, this.transform.Find("PortalPoints").transform.Find("Down").position, Quaternion.identity);
            portal.tag = "Down";
            portal.gameObject.SetActive(true);
            portal.transform.parent = room.transform;
        }
    }
    // OpenDoor메소드는 생성된 Room에서 생성할 문의 방향을 매개변수로 받아서 해당 문을 여는 메소드
    public Vector3 CalCulateCenter() {
        return this.transform.position;
    }
    private void portalDeAactive() {
        GameObject[] portals = FindObjectsWithName("PortalPrefab(Clone)");
        foreach (GameObject objs in portals) {
            objs.SetActive(false);
        }
    }
    private void portalActive() {
        GameObject[] portals = FindObjectsWithName("PortalPrefab(Clone)");
        foreach (GameObject objs in portals) {
            objs.SetActive(true);
        }
    }
    private GameObject[] FindObjectsWithName(string name) {
        // 하위 오브젝트들을 저장할 배열
        GameObject[] foundObjects;

        // 현재 게임 오브젝트의 모든 하위 오브젝트를 가져옴
        Transform[] childTransforms = this.GetComponentsInChildren<Transform>(true);

        // 특정 이름을 가진 오브젝트의 개수를 확인
        int count = 0;
        foreach (Transform childTransform in childTransforms) {
            if (childTransform.gameObject.name == name) {
                count++;
            }
        }

        // 배열 초기화
        foundObjects = new GameObject[count];
        int index = 0;

        // 특정 이름을 가진 오브젝트들을 배열에 저장
        foreach (Transform childTransform in childTransforms) {
            if (childTransform.gameObject.name == name) {
                foundObjects[index] = childTransform.gameObject;
                index++;
            }
        }

        return foundObjects;
    }


    bool isEnemyInRoom() {
        if (GameObject.FindGameObjectWithTag("Enemy") != null) {
            return true;
        }
        else {
            return false;
        }

    }
}

