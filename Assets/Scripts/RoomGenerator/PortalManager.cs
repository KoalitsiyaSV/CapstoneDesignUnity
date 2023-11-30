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
    private GameObject Camera;
    
    
    private void Awake() {
        RoomManager = GameObject.Find("RoomManager");
        Player = GameObject.Find("Player");
        Camera = GameObject.Find("Main Camera");
    }
    private void Start() {
        Vector2Int GridSize = RoomManager.GetComponent<RoomManager>().RoomGridSize;
        playerSpawnPoint = new Vector2Int(GridSize.x / 2, GridSize.y / 2);
        currentPlayerPosition = playerSpawnPoint;
    }
    public void PlayerTeleportation(GameObject entry) {
        if(entry.CompareTag("Left")) {
            currentPlayerPosition.x--;
            Room destination = RoomManager.GetComponent<RoomManager>().Teleport(currentPlayerPosition);
            foreach(Transform objects in destination.transform) {
                if(objects.gameObject.CompareTag("Right")) {
                    Player.transform.position = objects.gameObject.transform.position;
                    Camera.GetComponent<TempCamera>().CameraMove(destination.CalCulateCenter());
                }
            }
        }
        else if (entry.CompareTag("Right")) {
            currentPlayerPosition.x++;
            Room destination = RoomManager.GetComponent<RoomManager>().Teleport(currentPlayerPosition);
            foreach (Transform objects in destination.transform) {
                if (objects.gameObject.CompareTag("Left")) {
                    Player.transform.position = objects.gameObject.transform.position;
                    Camera.GetComponent<TempCamera>().CameraMove(destination.CalCulateCenter());
                }
            }
        }
        else if (entry.CompareTag("Up")) {
            currentPlayerPosition.y++;
            Room destination = RoomManager.GetComponent<RoomManager>().Teleport(currentPlayerPosition);
            foreach (Transform objects in destination.transform) {
                if (objects.gameObject.CompareTag("Down")) {
                    Player.transform.position = objects.gameObject.transform.position;
                    Camera.GetComponent<TempCamera>().CameraMove(destination.CalCulateCenter());
                }
            }
        }
        else if (entry.CompareTag("Down")) {
            currentPlayerPosition.y--;
            Room destination = RoomManager.GetComponent<RoomManager>().Teleport(currentPlayerPosition);
            foreach (Transform objects in destination.transform) {
                if (objects.gameObject.CompareTag("Up")) {
                    Player.transform.position = objects.gameObject.transform.position;
                    Camera.GetComponent<TempCamera>().CameraMove(destination.CalCulateCenter());
                }
            }
        }
    }
}
