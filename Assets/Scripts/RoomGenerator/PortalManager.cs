using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// PortalManager�� �̿��� Portal�� ���� Player�� �����̴� ����
public class PortalManager : MonoBehaviour
{
    private Vector2Int playerSpawnPoint;  // Player�� ������ �������� �� ��ȯ�Ǵ� ���� ��ġ
    private Vector2Int currentPlayerPosition; // Player�� ���� ��ġ
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
    
    // PlayerTeleportation�� Player�� �̿��� Portal�� �̸��� �Ű������� �޾Ƽ� Player�� �̵�
    public void PlayerTeleportation(string entryPortalName) {
        if(entryPortalName == "upPortal") { // Player�� �� Portal�� ������ ���
            currentPlayerPosition.y++;
            Vector3 destination = RoomManager.GetComponent<RoomManager>().teleportRoom(currentPlayerPosition, "down");
            Player.transform.SetPositionAndRotation(destination, Quaternion.identity); 
            // ���� ��ġ���� ���� Room�� �Ʒ� Portal�� �������� �����ϰ� Player�� �̵�
        }
        else if (entryPortalName == "downPortal") { //Player�� ���� Portal�� ������ ���
            currentPlayerPosition.y--;
            Vector3 destination = RoomManager.GetComponent<RoomManager>().teleportRoom(currentPlayerPosition, "up");
            Player.transform.SetPositionAndRotation(destination, Quaternion.identity);
            // ���� ��ġ���� ���� Room�� ���� Portal�� �������� �����ϰ� Player�� �̵�
        }
        else if (entryPortalName.Contains("Right")) {
            currentPlayerPosition.x++;
            Vector3 destination = RoomManager.GetComponent<RoomManager>().teleportRoom(currentPlayerPosition, entryPortalName);
            Player.transform.SetPositionAndRotation(destination, Quaternion.identity);
        }
        else if (entryPortalName.Contains("Left")) {
            currentPlayerPosition.x--;
            Vector3 destination = RoomManager.GetComponent<RoomManager>().teleportRoom(currentPlayerPosition, entryPortalName);
            Player.transform.SetPositionAndRotation(destination, Quaternion.identity);
        }
    }
}
