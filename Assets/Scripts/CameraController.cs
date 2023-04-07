using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private  Vector3 cameraPosition = new Vector3(0, 0, -10);

    [Header("Target")]
    public GameObject player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = player.transform.position + cameraPosition;
    }
}