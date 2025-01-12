using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private Animator anim;
    public bool isFlipped = false;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void SetOpenCardAnim()
    {
        anim.ResetTrigger("Idle");
        anim.SetTrigger("Open");
    }
    public void SetCloseCardAnim() => anim.SetTrigger("Close");
    public void DisableQuestionMark() => transform.Find("Question").gameObject.SetActive(false);
    public void EnableQuestionMark() => transform.Find("Question").gameObject.SetActive(true);
    public void EnableRedFx() => transform.Find("RedFx").gameObject.SetActive(true);
    public void DisableRedFx() => transform.Find("RedFx").gameObject.SetActive(false);
    public void SetOpenedState()
    {
        transform.Find("Tick").gameObject.SetActive(true);
        isFlipped = true;
    }
    public void SetClosedState()
    {
        transform.Find("Tick").gameObject.SetActive(false);
        isFlipped = false;
    }
    public void ResetStateOnResetGame()
    {
        EnableQuestionMark();
        SetClosedState();
        DisableRedFx();
        anim.SetTrigger("Idle");
    }
}
