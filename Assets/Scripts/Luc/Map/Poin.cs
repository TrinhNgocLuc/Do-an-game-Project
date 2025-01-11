using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poin : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
     private Vector3 desiredPosition;
   private void Update()
    {

        desiredPosition = target.position + offset;
        transform.position = desiredPosition;

    }
}
