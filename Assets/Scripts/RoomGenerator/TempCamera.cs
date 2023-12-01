using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCamera : MonoBehaviour {

    public void CameraMove(Vector3 position) {
        position.z = -1;
        this.transform.position = position;
        this.transform.rotation = Quaternion.identity;
    }
    private void Update() {
        
    }
}