using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public int changeSceneIndex = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    //private void OnTriggerStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("외않됌");
    //        if (Input.GetKeyDown(KeyCode.S))
    //        {
    //            Debug.Log("시발");
    //            Change();
    //        }
    //    }
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Change();
        }
    }

    // Update is called once per frame
    public void Change()
    {
        SceneManager.LoadScene(changeSceneIndex);
    }
}