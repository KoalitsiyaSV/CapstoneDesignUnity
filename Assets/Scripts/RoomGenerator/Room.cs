using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Room ��ũ��Ʈ�� ������ Room�� �����ϴ� ��ũ��Ʈ

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
    // OpenDoor�޼ҵ�� ������ Room���� ������ ���� ������ �Ű������� �޾Ƽ� �ش� ���� ���� �޼ҵ�
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
        // ���� ������Ʈ���� ������ �迭
        GameObject[] foundObjects;

        // ���� ���� ������Ʈ�� ��� ���� ������Ʈ�� ������
        Transform[] childTransforms = this.GetComponentsInChildren<Transform>(true);

        // Ư�� �̸��� ���� ������Ʈ�� ������ Ȯ��
        int count = 0;
        foreach (Transform childTransform in childTransforms) {
            if (childTransform.gameObject.name == name) {
                count++;
            }
        }

        // �迭 �ʱ�ȭ
        foundObjects = new GameObject[count];
        int index = 0;

        // Ư�� �̸��� ���� ������Ʈ���� �迭�� ����
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
            Debug.Log("Enemy Detected");
            return true;
        }
        else {
            Debug.Log("No Enemy Detected");
            return false;
        }

    }
}

