using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

// PortalController�� Portal�� ����� ��� ��ũ��Ʈ
public class PortalController : MonoBehaviour
{
    [SerializeField] bool canPlayerTeleport;
    private bool isPortalActive;
    private GameObject PortalManager;


    private void Awake() {
        PortalManager = GameObject.Find("PortalManager");

        canPlayerTeleport = false;
        isPortalActive = true;
    }
    private void Update() {
        if (canPlayerTeleport && Input.GetKeyDown(KeyCode.W)) {
            PortalManager.GetComponent<PortalManager>().PlayerTeleportation(this.gameObject);
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
