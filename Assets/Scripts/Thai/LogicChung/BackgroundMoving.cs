using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackgroundMoving : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private float startPosX;
    private bool canMove = false;
    private Canvas canvas;
    void Start()
    {
        startPosX = transform.position.x;
        canvas = transform.parent.GetComponent<Canvas>();
    }
    void Update()
    {
        if(canMove && Time.timeScale != 0f)
        {
            transform.Translate(new Vector2(speed, 0f));
            if (Mathf.Abs(transform.position.x - startPosX) > transform.GetChild(0).GetComponent<RectTransform>().rect.width * canvas.scaleFactor)
            {
                transform.position = new Vector2(startPosX, transform.position.y);
            }
        }
    }
    public void MoveBG() => canMove = true;
    public void StopMoveBG() => canMove = false;
}
