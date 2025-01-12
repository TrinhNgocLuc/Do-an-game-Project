using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTimeFx : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnFinishAnim()
    {
        GameManagerDaoAnh.Instance.AddBonusTime();
        gameObject.SetActive(false);
    }
}
