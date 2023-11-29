using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] protected GameObject targetObject;
    [SerializeField] protected Transform overheadImage;

    protected void Start()
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

            targetObject = collision.gameObject;

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

            if(targetObject != null)
            {
                targetObject = null;
            }
        }
    }
}
