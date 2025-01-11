using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BGsLocation
{
    left,
    center,
    right
}
public class BGLocation : MonoBehaviour
{
    [SerializeField] private BGsLocation bGLocation = BGsLocation.center;
    private BoxCollider2D boxCol;
    [SerializeField] private float distanceX;
    [SerializeField] private float canvasScaleFactor;
    private RectTransform rectTransform;
    private Canvas canvas;

    void Start()
    {
        canvas = transform.parent.parent.GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        boxCol = GetComponent<BoxCollider2D>();

        distanceX = rectTransform.rect.width * canvas.scaleFactor;
        if (this.bGLocation == BGsLocation.left)
            transform.position = new Vector2(transform.position.x - distanceX, transform.position.y);
        else if (this.bGLocation == BGsLocation.right)
            transform.position = new Vector2(transform.position.x + distanceX, transform.position.y);
    }
    void Update()
    {
        canvasScaleFactor = canvas.scaleFactor;
    }
}
