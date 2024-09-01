using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapControl : MonoBehaviour
{
    public GameObject minimapRenderer;

    private void Start()
    {
        //minimapRenderer = new GameObject();    
    }

    private void Update()
    {
        if (minimapRenderer != null)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                minimapRenderer.SetActive(true);
            }

            if (Input.GetKeyUp(KeyCode.Tab))
            {
                minimapRenderer.SetActive(false);
            }
        }
    }
}
