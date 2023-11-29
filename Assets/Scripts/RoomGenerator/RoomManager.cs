using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour {
    [SerializeField] GameObject roomPrefab;
    [SerializeField] GameObject[] roomPrefabList;
    [SerializeField] private int maxRooms = 15;
    [SerializeField] private int minRooms = 10;


    int roomWidth = 100;
    int roomHeight = 80;

    int[] roomPrefabCount;

    [SerializeField] int gridSizeX = 15;
    [SerializeField] int gridSizeY = 15;

    public Vector2Int RoomGridSize { 
        get { return new Vector2Int(gridSizeX, gridSizeY); } 
    }

    private List<GameObject> roomObjects = new List<GameObject>(); //생성된 방의 정보가 담김

    private Queue<Vector2Int> roomQueue = new Queue<Vector2Int>(); //생성 예정인 방의 위치가 저장됨

    private int[,] roomGrid; //모든 방은 roomGrid안에 생성됨

    private int roomCount;

    private bool generationComplete = false;

    private bool checkOverlap = false;
   
    private void Start() { // 1번 방 생성
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue = new Queue<Vector2Int>();

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
        roomPrefabCountReset();
    }
    private void Update() { // minRooms 이상, maxRooms 이하로 방 생성
        if (roomQueue.Count > 0 && roomCount < maxRooms && !generationComplete) {
            Vector2Int roomIndex = roomQueue.Dequeue();
            int gridX = roomIndex.x;
            int gridY = roomIndex.y;

            TryGenerateRoom(new Vector2Int(gridX - 1, gridY));
            TryGenerateRoom(new Vector2Int(gridX + 1, gridY));
            TryGenerateRoom(new Vector2Int(gridX, gridY + 1));
            TryGenerateRoom(new Vector2Int(gridX, gridY - 1));
        }
        else if (roomCount < minRooms) {
            Debug.Log("RoomCount was less than the minumum amount of rooms. Regenerating Room");
            RegenerateRooms();
        }
        else if(!generationComplete && !checkOverlap) {
            for (int i = 1; i < roomObjects.Count; i++) {
                if (roomObjects[0].transform.position == roomObjects[i].transform.position) {
                    Debug.Log("Overlap Detected. Regenerating Room");
                    checkOverlap = false;
                    RegenerateRooms();
                    break;
                }
                else
                    Debug.Log("Checking Overlap /// No Overlap detected in this generation");
                    checkOverlap = true;
            }
        }
        else if (!generationComplete && checkOverlap) {
            Debug.Log($"Generation Complete, {roomCount} room created");
            generationComplete = true;
        }
    }
     
    private void StartRoomGenerationFromRoom(Vector2Int roomIndex) { // 1번방 생성 메소드
        roomQueue.Enqueue(roomIndex);
        int x = roomIndex.x;
        int y = roomIndex.y;
        roomGrid[x, y] = 1;
        roomCount++;
        var initialRoom = Instantiate(roomPrefabList[0], GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        initialRoom.name = $"Room-{roomCount}";
        initialRoom.GetComponent<Room>().RoomIndex = roomIndex;
        roomObjects.Add(initialRoom);
    }

    private bool TryGenerateRoom(Vector2Int roomIndex) { // 2번방 ~ 이후 생성 메소드
        GameObject prefab;
        int x = roomIndex.x;
        int y = roomIndex.y;

        if (roomCount >= maxRooms)
            return false;

        if (Random.value < 0.5f && roomIndex != Vector2Int.zero)
            return false;

        if (CountAdjacentRooms(roomIndex) > 1)
            return false;

        roomQueue.Enqueue(roomIndex);
        roomGrid[x, y] = 1;
        roomCount++;
        while(true) {
            int i = Random.Range(1, roomPrefabList.Length);
            if(roomPrefabCount[i] < 2) {
                prefab = roomPrefabList[i];
                roomPrefabCount[i]++;
                break;
            }
        }
        var newRoom = Instantiate(prefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        newRoom.GetComponent<Room>().RoomIndex = roomIndex;
        newRoom.name = $"Room-{roomCount}";
        roomObjects.Add(newRoom);

        OpenDoors(newRoom, x, y);
        
        return true;
    }
    void OpenDoors(GameObject room, int x, int y) {
        Room newRoomScript = room.GetComponent<Room>();
        //Neighbours
        Room leftRoomScript = GetRoomScriptAt(new Vector2Int(x - 1, y));
        Room rightRoomScript = GetRoomScriptAt(new Vector2Int(x + 1, y));
        Room upRoomScript = GetRoomScriptAt(new Vector2Int(x, y + 1));
        Room downRoomScript = GetRoomScriptAt(new Vector2Int(x, y - 1));

        //Determine which doors to open based on the direction
        if (x > 0 && roomGrid[x - 1, y] != 0) {
            //Neighbouring room to the left
            newRoomScript.OpenDoor(Vector2Int.left, newRoomScript);
            leftRoomScript.OpenDoor(Vector2Int.right, leftRoomScript);            
        }
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0) {
            //Neighbouring room to the right
            newRoomScript.OpenDoor(Vector2Int.right, newRoomScript);
            rightRoomScript.OpenDoor(Vector2Int.left, rightRoomScript);
        }
        if (y > 0 && roomGrid[x, y - 1] != 0) {
            //Neighbouring room to the bottom
            newRoomScript.OpenDoor(Vector2Int.down, newRoomScript);
            downRoomScript.OpenDoor(Vector2Int.up, downRoomScript);
        }
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0) {
            //Neighbouring room to the top
            newRoomScript.OpenDoor(Vector2Int.up, newRoomScript);
            upRoomScript.OpenDoor(Vector2Int.down, upRoomScript);
        }
    }

    private void RegenerateRooms() {
        roomPrefabCountReset();
        roomObjects.ForEach(Destroy);
        roomObjects.Clear();
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue.Clear();
        roomCount = 0;
        generationComplete = false;

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
    }

    public Room GetRoomScriptAt(Vector2Int index) {
        GameObject roomObject = roomObjects.Find(r => r.GetComponent<Room>().RoomIndex == index);
        if (roomObject != null)
            return roomObject.GetComponent<Room>();
        return null;
    }

    private int CountAdjacentRooms(Vector2Int roomIndex) {
        int x = roomIndex.x;
        int y = roomIndex.y;
        int count = 0;

        if (x > 0 && roomGrid[x - 1, y] != 0) count++; //right neighbour
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0) count++; //right neighbour
        if (y > 0 && roomGrid[x, y - 1] != 0) count++; //Botton neighbour
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0) count++; //Top neighbour

        return count;
    }

    private Vector3 GetPositionFromGridIndex(Vector2Int gridIndex) {
        int gridX = gridIndex.x;
        int gridY = gridIndex.y;

        return new Vector3(roomWidth * (gridX - gridSizeX / 2), roomHeight * (gridY - gridSizeY / 2));
        //gridIndex를 바탕으로 Room의 position을 찾아줌
    }
    private void OnDrawGizmos() {
        Color gizmoColor = new Color(0, 1, 1, 0.05f);
        Gizmos.color = gizmoColor;

        for (int x = 0; x < gridSizeX; x++) {
            for (int y = 0; y < gridSizeY; y++) {
                Vector3 position = GetPositionFromGridIndex(new Vector2Int(x, y));
                Gizmos.DrawWireCube(position, new Vector3(roomWidth, roomHeight, 1));
            }
        }
    }
   
    public Room Teleport(Vector2Int targetRoomGrid) {
        Room targetRoom = GetRoomScriptAt(targetRoomGrid);
        
        return targetRoom;
    }
    // 텔레포트할 방의 Vector2Int 값과 목적지 Portal의 이름을 매개변수로 받아서 목적지 Portal의 좌표를 return함

    private void roomPrefabCountReset() {
        roomPrefabCount = new int[roomPrefabList.Length];
        for (int i = 1; i < roomPrefabList.Length; i++) {
            roomPrefabCount[i] = 0;
        }
    }
}
