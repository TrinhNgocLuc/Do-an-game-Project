using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkConnect : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] GameObject Location;
    [SerializeField] GameObject Select_Library;
    [SerializeField] GameObject Select_1;
    [SerializeField] GameObject Select_2;
    [SerializeField] GameObject Select_3;
    [SerializeField] GameObject Select_4;
    [SerializeField] GameObject Select_5;
    [SerializeField] GameObject Select_6;

    void Start()
    {
       Select_Library.SetActive(false);
        Select_1.SetActive(false);
        Select_2.SetActive(false);
        Select_3.SetActive(false);
        Select_4.SetActive(false);
        Select_5.SetActive(false);
        Select_6.SetActive(false);
    }
    void Update()
    {
        CheckConnect();
    }
    public void CheckConnect()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            Location = hit.transform.gameObject;
            if (Location.gameObject.CompareTag("Library"))
            {
                Select_Library.SetActive(true);
            }
            else if (Location.gameObject.CompareTag("Game1"))
            {
                Select_1.SetActive(true);
            }
            else if (Location.gameObject.CompareTag("Game2"))
            {
                Select_2.SetActive(true);
            }
            else if (Location.gameObject.CompareTag("Game3"))
            {
                Select_3.SetActive(true);
            }
            else if (Location.gameObject.CompareTag("Game4"))
            {
                Select_4.SetActive(true);
            }
            else if (Location.gameObject.CompareTag("Game5"))
            {
                Select_5.SetActive(true);
            }
            else if (Location.gameObject.CompareTag("Game5"))
            {
                Select_6.SetActive(true);
            }
        }
        else
        {
            Select_Library.SetActive(false);
            Select_1.SetActive(false);
            Select_2.SetActive(false);
            Select_3.SetActive(false);
            Select_4.SetActive(false);
            Select_5.SetActive(false);
            Select_6.SetActive(false);

        }
    }
}
