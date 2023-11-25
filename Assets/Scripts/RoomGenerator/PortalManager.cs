using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// PortalManager는 이용한 Portal에 따라 Player을 움직이는 역할
public class PortalManager : MonoBehaviour
{
    private Vector2Int playerSpawnPoint;  // Player가 던전에 입장했을 때 소환되는 방의 위치
    private Vector2Int currentPlayerPosition; // Player의 현재 위치
    private GameObject RoomManager;
    private GameObject Player;
    
    private void Awake() {
        RoomManager = GameObject.Find("RoomManager");
        Player = GameObject.Find("Player");
    }
    private void Start() {
        Vector2Int GridSize = RoomManager.GetComponent<RoomManager>().RoomGridSize;
        playerSpawnPoint = new Vector2Int(GridSize.x / 2, GridSize.y / 2);
        currentPlayerPosition = playerSpawnPoint;
    }
    
    // PlayerTeleportation은 Player가 이용한 Portal의 이름을 매개변수로 받아서 Player을 이동
    public void PlayerTeleportation(string portalName) {
        if(portalName == "upPortal") { // Player가 위 Portal로 입장한 경우
            currentPlayerPosition.y++;
            Vector3 destination = RoomManager.GetComponent<RoomManager>().teleportRoom(currentPlayerPosition, "down");
            Player.transform.SetPositionAndRotation(destination, Quaternion.identity); 
            // 현재 위치에서 위쪽 Room의 아래 Portal을 목적지로 설정하고 Player을 이동
        }
        else if (portalName == "downPortal") { //Player가 좌측 Portal로 입장한 경우
            currentPlayerPosition.y--;
            Vector3 destination = RoomManager.GetComponent<RoomManager>().teleportRoom(currentPlayerPosition, "up");
            Player.transform.SetPositionAndRotation(destination, Quaternion.identity);
            // 현재 위치에서 좌측 Room의 우측 Portal을 목적지로 설정하고 Player을 이동
        }
        else if (portalName == "rightPortal") {
            currentPlayerPosition.x++;
            Vector3 destination = RoomManager.GetComponent<RoomManager>().teleportRoom(currentPlayerPosition, "left");
            Player.transform.SetPositionAndRotation(destination, Quaternion.identity);
        }
        else if (portalName == "leftPortal") {
            currentPlayerPosition.x--;
            Vector3 destination = RoomManager.GetComponent<RoomManager>().teleportRoom(currentPlayerPosition, "right");
            Player.transform.SetPositionAndRotation(destination, Quaternion.identity);
        }
    }
}
