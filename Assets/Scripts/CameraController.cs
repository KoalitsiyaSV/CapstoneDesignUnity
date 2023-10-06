using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Range")]
    public float MaxX = 0;
    public float MinX = 0;
    public float MaxY = 0;
    public float MinY = 0;

    [Header("test")]
    public float CurX = 0;
    public float CurY = 0;

    private Vector3 cameraPosition = new Vector3(0, 2, -10);

    // 카메라 너비, 높이
    private float cameraHalfWidth, cameraHalfHeight;

    void Start()
    {
        //카메라 범위 제한을 위한 카메라의 너비와 높이 구하기
        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        cameraHalfHeight = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //카메라 범위 제한
        //800x450에서는 y축에 3f를 더하면 적당한듯
        Vector3 desiredPosition = new Vector3(
            Mathf.Clamp(player.position.x + cameraPosition.x, MinX + cameraHalfWidth, MaxX - cameraHalfWidth),
            Mathf.Clamp(player.position.y + cameraPosition.y, MinY + cameraHalfHeight, MaxY - cameraHalfHeight) + 3f,
            cameraPosition.z);

        //카메라의 이동
        //transform.position = player.transform.position + cameraPosition;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, 
                          Time.deltaTime *2f);

        CurX = transform.position.x;
        CurY = cameraPosition.y;
    }
}