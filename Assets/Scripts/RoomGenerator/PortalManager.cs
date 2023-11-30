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
    [SerializeField] GameObject roomManager;
    
    
    private void Awake() {
        RoomManager = GameObject.Find("RoomManager");
        Player = GameObject.Find("Player");
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
            Room destination = RoomManager.GetComponent<RoomManager>().Teleport(currentPlayerPosition);
            destination.gameObject.SetActive(true);
            foreach(Transform objects in destination.transform) {
                if(objects.gameObject.CompareTag("Right")) {
                    Vector3 position = new Vector3(objects.position.x, objects.position.y + positionRevision, objects.position.z);
                    Player.transform.position = position;
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
                    Player.transform.position = position;
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
                    Player.transform.position = position;
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
                    Player.transform.position = position;
                }
            }
        }
    }
}
