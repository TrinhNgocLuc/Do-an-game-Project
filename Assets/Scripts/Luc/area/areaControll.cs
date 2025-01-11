using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class areaControll : MonoBehaviour
{
    public GameObject option_1;
    public GameObject option_2;
    public GameObject SelectOption;
    [SerializeField] private AudioSource SfxButton;
    private bool statusOption1 = false;
    private bool statusOption2 = false;
    public void Start()
    {
        option_1.SetActive(false);
        option_2.SetActive(false);
    }

    public void controllOption1()
    {
        SfxButton.Play();
        statusOption1 = !statusOption1;
        if (statusOption1 == true)
        {
            option_1.SetActive(true);
            SelectOption.SetActive(false);
        }
        else 
        {
            option_1.SetActive(false);
            SelectOption.SetActive(true);
        }
    }
    public void controllOption2()
    {
        SfxButton.Play();
        statusOption2 = !statusOption2;
        if (statusOption2 == true)
        {
            option_2.SetActive(true);
            SelectOption.SetActive(false);

        }
        else
        {
            option_2.SetActive(false);
            SelectOption.SetActive(true);

        }
    }
}
