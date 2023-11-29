using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCManager : MonoBehaviour
{
    [SerializeField] GameObject[] npcFuctions;

    private static NPCManager _instance = null;

    public static NPCManager instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject npcMgr = new GameObject("NPCManager");
                _instance = npcMgr.AddComponent<NPCManager>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void activeNpcFunction()
    {
        npcFuctions[0].SetActive(true);
    }
}