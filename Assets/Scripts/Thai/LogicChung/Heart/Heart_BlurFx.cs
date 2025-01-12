using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Minigame
{
    SXChuCai,
    DaoAnh,
    LuyenTriNho,
    TanCongTuVung
}
public class Heart_BlurFx : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Minigame minigame = Minigame.SXChuCai;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnFinishAnim()
    {
        switch (minigame)
        {
            case Minigame.SXChuCai:
                GameManager_SXChuCai.instance.EnableEndGameUI();
                break;
            case Minigame.DaoAnh:
                GameManagerDaoAnh.Instance.EnableEndGameUI();
                break;
            case Minigame.LuyenTriNho:
                GameManagerLuyenTriNho.Instance.EnableEndGameUI();
                break;
            case Minigame.TanCongTuVung:
                GameManagerTanCongTuVung.Instance.EnableEndGameUI();
                break;
        }
        gameObject.SetActive(false);
    }
}
