using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddScoreFx : MonoBehaviour
{
    private Animator anim;
    [HideInInspector] public TextMeshProUGUI addScoreText;
    [SerializeField] private Minigame minigame = Minigame.SXChuCai;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        addScoreText = GetComponent<TextMeshProUGUI>();
    }
    private void OnFinishAnim()
    {
        if(minigame == Minigame.SXChuCai)
            GameManager_SXChuCai.instance.AddBonusScore();
        else if(minigame == Minigame.DaoAnh) 
            GameManagerDaoAnh.Instance.AddBonusScore();
        else if(minigame == Minigame.LuyenTriNho)
            GameManagerLuyenTriNho.Instance.AddBonusScore();
        gameObject.SetActive(false);
    }
}
