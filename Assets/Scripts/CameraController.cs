using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private  Vector3 cameraPosition = new Vector3(0, 2, -10);

    [Header("Target")]
    public GameObject player;

    [Header("Range")]
    public float MaxX = 0;
    public float MinX = 0;
    public float MaxY = 0;
    public float MinY = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = player.transform.position + cameraPosition;
        transform.position = Vector3.Lerp(transform.position, player.transform.position + cameraPosition, 
                          Time.deltaTime *2f);
    }
}