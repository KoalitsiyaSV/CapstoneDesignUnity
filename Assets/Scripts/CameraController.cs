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
    public float nowX = 0;
    public float nowY = 0;

    private Vector3 cameraPosition = new Vector3(0, 2, -10);

    // ī�޶� �ʺ�, ����
    private float cameraHalfWidth, cameraHalfHeight;

    void Start()
    {
        //ī�޶� ���� ������ ���� ī�޶��� �ʺ�� ���� ���ϱ�
        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        cameraHalfHeight = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //ī�޶� ���� ����
        //800x450������ y�࿡ 3f�� ���ϸ� �����ѵ�
        Vector3 desiredPosition = new Vector3(
            Mathf.Clamp(player.position.x + cameraPosition.x, MinX + cameraHalfWidth, MaxX - cameraHalfWidth),
            Mathf.Clamp(player.position.y + cameraPosition.y, MinY + cameraHalfHeight, MaxY - cameraHalfHeight) + 3f,
            cameraPosition.z);

        nowX = cameraPosition.x;
        nowY = cameraPosition.y;

        //ī�޶��� �̵�
        //transform.position = player.transform.position + cameraPosition;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, 
                          Time.deltaTime *2f);
    }
}