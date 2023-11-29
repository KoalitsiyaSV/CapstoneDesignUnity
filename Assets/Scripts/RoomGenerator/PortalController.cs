using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PortalController�� Portal�� ����� ��� ��ũ��Ʈ
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

        } // Player�� Portal�� �����ϰ�, W�� ������ ������ Portal�� �̸��� �Ű������� PlayerTeleportation�� ȣ��.
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