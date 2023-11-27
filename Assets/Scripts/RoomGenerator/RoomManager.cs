using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour {
    [SerializeField] GameObject roomPrefab;
    [SerializeField] private int maxRooms = 15;
    [SerializeField] private int minRooms = 7;

    int roomWidth = 130;
    int roomHeight = 100;

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

    private string[] portalPosition = new string[] { "lower", "upper", "basic" };

   
    private void Start() {
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue = new Queue<Vector2Int>();

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
    }
    private void Update() {
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
            Debug.Log("RoomCount was less than the minumum amount of rooms. Trying again");
            RegenerateRooms();
        }
        else if (!generationComplete) {
            Debug.Log($"Generation Complete, {roomCount} room created");
            generationComplete = true;
        }
    }
     
    private void StartRoomGenerationFromRoom(Vector2Int roomIndex) {
        roomQueue.Enqueue(roomIndex);
        int x = roomIndex.x;
        int y = roomIndex.y;
        roomGrid[x, y] = 1;
        roomCount++;
        var initialRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        initialRoom.name = $"Room-{roomCount}";
        initialRoom.GetComponent<Room>().RoomIndex = roomIndex;
        roomObjects.Add(initialRoom);
    }

    private bool TryGenerateRoom(Vector2Int roomIndex) {
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

        var newRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
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
            int i = Random.Range(0, 3);
            newRoomScript.OpenLRDoor(Vector2Int.left, portalPosition[i]);
            leftRoomScript.OpenLRDoor(Vector2Int.right, portalPosition[i]);            
        }
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0) {
            //Neighbouring room to the right
            int i = Random.Range(0, 3);
            newRoomScript.OpenLRDoor(Vector2Int.right, portalPosition[i]);
            rightRoomScript.OpenLRDoor(Vector2Int.left, portalPosition[i]);
        }
        if (y > 0 && roomGrid[x, y - 1] != 0) {
            //Neighbouring room to the bottom
            newRoomScript.OpenDoor(Vector2Int.down);
            downRoomScript.OpenDoor(Vector2Int.up);
        }
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0) {
            //Neighbouring room to the top
            newRoomScript.OpenDoor(Vector2Int.up);
            upRoomScript.OpenDoor(Vector2Int.down);
        }
    }

    private void RegenerateRooms() {
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
    public Vector3 teleportRoom(Vector2Int targetRoomGrid, string entryPortal) {
        Room targetRoom = GetRoomScriptAt(targetRoomGrid);
        Vector3 target = new Vector3();
        if (entryPortal == "down") {
            target = targetRoom.getDownPortalPosition;
        }
        else if (entryPortal == "up") {
            target = targetRoom.getUpPortalPosition;
        }
        else if (entryPortal.Contains("Right")) {
            if(entryPortal.Contains("lower"))
                target = targetRoom.getLeftPortalPosition("lower");
            else if(entryPortal.Contains("upper"))
                target = targetRoom.getLeftPortalPosition("upper");
            else if(entryPortal.Contains("basic"))
                target = targetRoom.getLeftPortalPosition("basic");
        }
        else if (entryPortal.Contains("Left")) {
            if (entryPortal.Contains("lower"))
                target = targetRoom.getRightPortalPosition("lower");
            else if (entryPortal.Contains("upper"))
                target = targetRoom.getRightPortalPosition("upper");
            else if (entryPortal.Contains("basic"))
                target = targetRoom.getRightPortalPosition("basic");
        }
        return target;
    }
    // 텔레포트할 방의 Vector2Int 값과 목적지 Portal의 이름을 매개변수로 받아서 목적지 Portal의 좌표를 return함
}
