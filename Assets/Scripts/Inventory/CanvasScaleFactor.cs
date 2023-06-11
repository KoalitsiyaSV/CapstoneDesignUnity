using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScaleFactor : MonoBehaviour
{
    Canvas canvas;

    private void Start()
    {
        canvas.GetComponent<Canvas>();
    }

    public float GetScaleFactor()
    {
        float scaleFactor = canvas.scaleFactor;

        return scaleFactor;
    }
}
