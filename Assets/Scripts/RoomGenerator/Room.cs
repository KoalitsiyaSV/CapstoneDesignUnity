using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Room ��ũ��Ʈ�� ������ Room�� �����ϴ� ��ũ��Ʈ
 
public class Room : MonoBehaviour {
    [SerializeField] GameObject portalPrefab;
    
    public Vector2Int RoomIndex { get; set; }

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
}

