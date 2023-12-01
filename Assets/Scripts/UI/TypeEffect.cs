using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeEffect : MonoBehaviour
{
    public float charPerSeconds;
    public GameObject endCursor;

    private string targetDialogue;
    [SerializeField] TextMeshProUGUI dialogueText;
    private int index;

    private void Awake()
    {
        dialogueText = GetComponent<TextMeshProUGUI>();
    }

    public void SetDialogue(string dialogue)
    {
        targetDialogue = dialogue;
        EffectStart();
    }

    private void EffectStart()
    {
        dialogueText.text = "";
        Debug.Log("Here");

        index = 0;
        endCursor.SetActive(false);

        Invoke("Effecting", 1 / charPerSeconds);
    }

    private void Effecting()
    {
        if(dialogueText.text == targetDialogue)
        {
            EffectEnd();
            return;
        }

        dialogueText.text += targetDialogue[index];
        index++;

        Invoke("Effecting", 1 / charPerSeconds);
    }

    private void EffectEnd()
    {
        endCursor.SetActive(true);
    }
}
