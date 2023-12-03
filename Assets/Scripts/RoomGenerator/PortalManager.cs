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
            Room destination = RoomManager.GetComponent<RoomManager>().Teleport(currentPlayerPosition); //�̰Ŵ� �� ���� �� ��ü �޾ƿ��°ſ���
            destination.gameObject.SetActive(true); //�� Ȱ��ȭ�ϰ�
            foreach(Transform objects in destination.transform) {//�׳� �ű⼭ right��
                if(objects.gameObject.CompareTag("Right")) {
                    Vector3 position = new Vector3(objects.position.x, objects.position.y + positionRevision, objects.position.z);
                    Debug.Log("teleport");
                    PlayerObj.transform.position = position; //�׳�
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
