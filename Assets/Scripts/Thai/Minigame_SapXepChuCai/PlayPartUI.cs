using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPartUI : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void SetOnState(bool state) => anim.SetBool("On", state);
}
