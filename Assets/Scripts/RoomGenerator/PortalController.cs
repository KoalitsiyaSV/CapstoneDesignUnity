using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PortalController는 Portal의 기능이 담긴 스크립트
public class PortalController : MonoBehaviour
{
    private bool canPlayerTeleport;
    private GameObject PortalManager;

    private void Awake() {
        PortalManager = GameObject.Find("PortalManager");
        canPlayerTeleport = false;
    }

    private void Update() {
        if (canPlayerTeleport && Input.GetKeyDown(KeyCode.UpArrow)) {
            Debug.Log(this.name);
            PortalManager.GetComponent<PortalManager>().PlayerTeleportation(this.name);

        } // Player가 Portal에 접촉하고, W를 누르면 입장한 Portal의 이름을 매개변수로 PlayerTeleportation을 호출.
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            canPlayerTeleport = true;
        }
    } 
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            canPlayerTeleport = false;
        }
    }
}
