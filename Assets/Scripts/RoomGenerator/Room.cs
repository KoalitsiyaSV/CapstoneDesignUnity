using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Room 스크립트는 생성된 Room을 제어하는 스크립트
 
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
    // OpenDoor메소드는 생성된 Room에서 생성할 문의 방향을 매개변수로 받아서 해당 문을 여는 메소드
    public Vector3 CalCulateCenter() {
        return this.transform.position;
    }
}

