using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float moveDistance; 

    private Vector3 startPosition;
    private float elapsedTime = 0f;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime * moveSpeed;
        float newY = startPosition.y + Mathf.Sin(elapsedTime) * moveDistance;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
