using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGFitter : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private void Awake()
    {
        Vector2 localScale = transform.localScale;
        if(canvas.scaleFactor < 1f)
            localScale.x /= canvas.scaleFactor;
        else
            localScale.x *= canvas.scaleFactor;
        transform.localScale = localScale;
        Debug.Log(canvas.scaleFactor);
    }
}
