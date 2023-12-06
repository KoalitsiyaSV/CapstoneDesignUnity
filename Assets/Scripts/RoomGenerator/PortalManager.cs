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
    [SerializeField] GameObject roomManager;
    [SerializeField] GameObject PlayerObj;
    
    
    private void Awake() {
        RoomManager = GameObject.Find("RoomManager");
    }
    private void Start() {
        Vector2Int GridSize = RoomManager.GetComponent<RoomManager>().RoomGridSize;
        playerSpawnPoint = new Vector2Int(GridSize.x / 2, GridSize.y / 2);
        currentPlayerPosition = playerSpawnPoint;
    }
    public void PlayerTeleportation(GameObject entry) {
        string exit = SwitchDirection(entry.tag);
        float positionRevision = 1.4f;
        if(entry.CompareTag("Left")) {
            currentPlayerPosition.x--;
        }
        else if (entry.CompareTag("Right")) {
            currentPlayerPosition.x++;
        }
        else if (entry.CompareTag("Up")) {
            currentPlayerPosition.y++;  
        }
        else if (entry.CompareTag("Down")) {
            currentPlayerPosition.y--;
        }
        Room destination = RoomManager.GetComponent<RoomManager>().Teleport(currentPlayerPosition);
        destination.gameObject.SetActive(true);
        foreach (Transform objects in destination.transform) {
            if (objects.gameObject.CompareTag(exit)) {
                Vector3 position = new Vector3(objects.position.x, objects.position.y + positionRevision, objects.position.z);
                PlayerObj.transform.position = position;
            }
        }
    }
    private string SwitchDirection(string inputDirection) {
        switch (inputDirection) {
            case "Left": return "Right";
            case "Right": return "Left";
            case "Up": return "Down";
            case "Down": return "Up";
            default: return "UNKNOWN DIRECTION";
        }
    }
}
