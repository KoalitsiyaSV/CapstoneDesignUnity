using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCamera : MonoBehaviour {
    // Start is called before the first frame update
    Vector3 location;

    [SerializeField] GameObject player;

    private void LateUpdate() {
        this.transform.position = Vector3.Lerp(this.transform.position, player.transform.position, 1f);
    }
    //private void OnCollisionEnter2D(Collision2D collision) {
    //    if (collision.gameObject.CompareTag("TDArea") || collision.gameObject.CompareTag("LRArea")) {
    //        Vector3 location = this.transform.position;
    //    }
        
    //}
    //private void OnCollisionStay2D(Collision2D collision) {
    //    if (collision.gameObject.CompareTag("TDArea") || collision.gameObject.CompareTag("LRArea")) {
    //        this.transform.position = location;
    //    }
    //}
}