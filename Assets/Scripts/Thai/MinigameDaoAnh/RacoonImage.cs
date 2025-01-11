using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RacoonImage : MonoBehaviour
{
    [SerializeField] private Image vocaImage;
    [SerializeField] private GameObject explosionFx;
    private void OnEnable()
    {
        ShowFx();
    }
    public void SetImage(Sprite s) 
    {
        vocaImage.sprite = s;
    }
    public Image GetImage() => vocaImage;
    public void SetToAnswerImageOnPowerUp(Sprite s) => vocaImage.sprite = s;
    public void ShowFx() => explosionFx.SetActive(true);
}
