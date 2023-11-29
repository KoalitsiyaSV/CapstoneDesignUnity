using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInterationDetector : PlayerDetector
{
    private void Update()
    {
        if (targetObject != null && Input.GetKeyDown(KeyCode.F));
        {
            GameManager.Instance.DialogueAction(targetObject);
        }
    }
}