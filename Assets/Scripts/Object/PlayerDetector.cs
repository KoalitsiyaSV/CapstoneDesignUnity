using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [Header("Detector Data")]
    [SerializeField] protected GameObject player;
    [SerializeField] protected Transform overheadImage;

    protected virtual void Start()
    {
        overheadImage = this.transform.Find("OverheadImage");

        if (overheadImage != null)
            overheadImage.gameObject.SetActive(false);
    }

    protected void Awake()
    {
        
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Detected");

            player = collision.gameObject;

            if (overheadImage != null)
                overheadImage.gameObject.SetActive(true);
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Escape Area");

            if(overheadImage != null)
            {
                overheadImage.gameObject.SetActive(false);
            }

            if(player != null)
            {
                player = null;
            }
        }
    }
}
