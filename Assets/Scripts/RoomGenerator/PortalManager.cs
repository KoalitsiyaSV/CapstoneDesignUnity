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
        float positionRevision = 3.6f;
        if(entry.CompareTag("Left")) {
            currentPlayerPosition.x--;
            Room destination = RoomManager.GetComponent<RoomManager>().Teleport(currentPlayerPosition); //이거는 그 옆방 룸 자체 받아오는거에요
            destination.gameObject.SetActive(true); //룸 활성화하고
            foreach(Transform objects in destination.transform) {//네네 거기서 right만
                if(objects.gameObject.CompareTag("Right")) {
                    Vector3 position = new Vector3(objects.position.x, objects.position.y + positionRevision, objects.position.z);
                    Debug.Log("teleport");
                    PlayerObj.transform.position = position; //네네
                }
            }
        }
        else if (entry.CompareTag("Right")) {
            currentPlayerPosition.x++;
            Room destination = RoomManager.GetComponent<RoomManager>().Teleport(currentPlayerPosition);
            destination.gameObject.SetActive(true);
            foreach (Transform objects in destination.transform) {
                if (objects.gameObject.CompareTag("Left")) {
                    Vector3 position = new Vector3(objects.position.x, objects.position.y + positionRevision, objects.position.z);
                    PlayerObj.transform.position = position;
                }
            }
        }
        else if (entry.CompareTag("Up")) {
            currentPlayerPosition.y++;
            Room destination = RoomManager.GetComponent<RoomManager>().Teleport(currentPlayerPosition);
            destination.gameObject.SetActive(true);
            foreach (Transform objects in destination.transform) {
                if (objects.gameObject.CompareTag("Down")) {
                    Vector3 position = new Vector3(objects.position.x, objects.position.y + positionRevision, objects.position.z);
                    PlayerObj.transform.position = position;
                }
            }
        }
        else if (entry.CompareTag("Down")) {
            currentPlayerPosition.y--;
            Room destination = RoomManager.GetComponent<RoomManager>().Teleport(currentPlayerPosition);
            destination.gameObject.SetActive(true);
            foreach (Transform objects in destination.transform) {
                if (objects.gameObject.CompareTag("Up")) {
                    Vector3 position = new Vector3(objects.position.x, objects.position.y + positionRevision, objects.position.z);
                    PlayerObj.transform.position = position;
                }
            }
        }
    }
}
