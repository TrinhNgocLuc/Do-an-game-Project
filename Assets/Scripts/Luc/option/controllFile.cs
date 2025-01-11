using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controllFile : MonoBehaviour
{
    [SerializeField] private File _file;
    [SerializeField] private AudioSource SfxButton;

    private int currentIndex = 0;
    [SerializeField] private Image spriteRenderer;
    void Start()
    {
        GetFile();
    }

    void GetFile()
    {
        spriteRenderer.sprite = _file.GetSprites()[currentIndex];

    }
    public void swapLeft()
    {
        SfxButton.Play();
        currentIndex--;
        if (currentIndex <0) 
        {
            currentIndex = 0;
            spriteRenderer.sprite = _file.GetSprites()[currentIndex];
        }
        else
        {
            spriteRenderer.sprite = _file.GetSprites()[currentIndex];

        }
    }
    public void swapRight()
    {
        SfxButton.Play();
        currentIndex++;
        if (currentIndex >29)
        {
            currentIndex = 29;
            spriteRenderer.sprite = _file.GetSprites()[currentIndex];
        }
        else
        {  
            spriteRenderer.sprite = _file.GetSprites()[currentIndex];
        }
    }
}
