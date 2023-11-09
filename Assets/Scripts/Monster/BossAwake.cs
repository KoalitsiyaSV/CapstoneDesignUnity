using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAwake : MonoBehaviour
{
   public Transform[] childObjects;

    // Start is called before the first frame update
    void Start()
    {
        Transform parentObject = transform;

        childObjects = parentObject.GetComponentsInChildren<Transform>(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            childObjects[1].gameObject.SetActive(true);
            childObjects[2].gameObject.SetActive(false);
        }
    }
}
