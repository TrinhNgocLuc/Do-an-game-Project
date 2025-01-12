using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Idle", true);
        if(GameManagerDaoAnh.Instance != null)
            GameManagerDaoAnh.Instance.onPlayerTouchingAction += SetPickUpAnim;
    }
    private void SetPickUpAnim()
    {
        if (TongsHandler.Instance.isPickingUp)
            return;
        SetAnim("Pick");
    }
    public void SetAnim(string _animName)
    {
        foreach(AnimatorControllerParameter para in anim.parameters)
        {
            anim.SetBool(para.name, false);
        }
        anim.SetBool(_animName, true);
    }
    private void OnFinshAnim() => SetAnim("Idle");
    public void Flip(bool facingRight)
    {
        Vector2 localScale = transform.parent.localScale;
        if (facingRight)
            localScale.x = 1.1f;
        else
            localScale.x = -1.1f;
        transform.parent.localScale = localScale;

    } 
}
