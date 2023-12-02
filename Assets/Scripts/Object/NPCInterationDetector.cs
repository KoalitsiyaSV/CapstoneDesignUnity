using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInterationDetector : PlayerDetector
{
    private ObjectData objData;

    protected override void Start()
    {
        base.Start();

        objData = GetComponent<ObjectData>();
    }

    private void Update()
    {
        if (player != null && Input.GetKeyDown(KeyCode.F))
        {
            GameManager.Instance.DialogueAction(objData);

            if (GameManager.Instance.isAction)
                overheadImage.gameObject.SetActive(false);
            else
                overheadImage.gameObject.SetActive(true);
        }
    }
}