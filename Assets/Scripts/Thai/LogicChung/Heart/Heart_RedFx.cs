using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart_RedFx : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnFinishAnim()
    {
        transform.parent.Find("Heart_BlurFx").gameObject.SetActive(true);
        transform.parent.GetComponent<Image>().color = new Color(0f, 0f, 0f);
        gameObject.SetActive(false);
    }
}
