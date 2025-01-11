using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphabetHolder : MonoBehaviour
{
    public static AlphabetHolder instance { get; private set; }
    private Difficulty currentMode = Difficulty.easy;
    [SerializeField] private int easyModeAlphabetMaxNum;
    [SerializeField] private int normalModeAlphabetMaxNum;
    [SerializeField] private int hardModeAlphabetMaxNum;
    private GridLayoutGroup gridLayoutGroup;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
    private void Start()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
    }
    void Update()
    {
        ChangeAlphabetNumByMode();
    }

    private void ChangeAlphabetNumByMode()
    {
        if (currentMode != DifficultyManager.instance.Mode)
        {
            currentMode = DifficultyManager.instance.Mode;
            DisableAllAlphabetUI();
            switch (DifficultyManager.instance.Mode)
            {
                case Difficulty.easy:
                    for (int i = 0; i < easyModeAlphabetMaxNum; i++)
                    {
                        transform.GetChild(i).gameObject.SetActive(true);
                    }
                    gridLayoutGroup.constraintCount = 4;
                    break;
                case Difficulty.normal:
                    for (int i = 0; i < normalModeAlphabetMaxNum; i++)
                    {
                        transform.GetChild(i).gameObject.SetActive(true);
                    }
                    gridLayoutGroup.constraintCount = 6;
                    break;
                case Difficulty.hard:
                    for (int i = 0; i < hardModeAlphabetMaxNum; i++)
                    {
                        transform.GetChild(i).gameObject.SetActive(true);
                    }
                    gridLayoutGroup.constraintCount = 12;
                    break;
            }
        }
    }
    private void DisableAllAlphabetUI()
    {
        foreach(Transform transf in transform)
        {
            transf.gameObject.SetActive(false);
        }
    }
    public void GetAlphabets()
    {
        string voca = GameManager_SXChuCai.instance.currentVocabulary.vocabulary;
        List<char> chars = new List<char>();
        List<int> remainIndexList = new List<int>();
        List<int> rdIndexList = new List<int>();
        switch (DifficultyManager.instance.Mode)
        {
            case Difficulty.easy:
                for(int i=0; i<easyModeAlphabetMaxNum; i++)
                {
                    remainIndexList.Add(i);
                }
                break;
            case Difficulty.normal:
                for (int i = 0; i < normalModeAlphabetMaxNum; i++)
                {
                    remainIndexList.Add(i);
                }
                break;
            case Difficulty.hard:
                for (int i = 0; i < hardModeAlphabetMaxNum; i++)
                {
                    remainIndexList.Add(i);
                }
                break;
        }
        List<char> rdChars = new List<char>();
        List<int> remainCharsIndex = new List<int>();
        int cnt = 0;
        foreach (char c in voca)
        {
            if (c == ' ')
                continue;
            chars.Add(c);
            remainCharsIndex.Add(cnt++);
            int rdIndex = Random.Range(0, remainIndexList.Count);
            rdIndexList.Add(remainIndexList[rdIndex]);
            remainIndexList.Remove(remainIndexList[rdIndex]);
        }
        for(int i=0; i<chars.Count; i++)
        {
            int rdIndex = Random.Range(0, remainCharsIndex.Count);
            rdChars.Add(chars[remainCharsIndex[rdIndex]]);
            remainCharsIndex.Remove(remainCharsIndex[rdIndex]);
        }
        // muc dich de co 2 cai: rdIndexList va rdChars
        int cnt1 = 0;
        foreach (Transform transf in transform)
        {
            transf.GetChild(0).GetComponent<Image>().sprite = GameManager_SXChuCai.instance.GetRandomAlphabetSprite();
            transf.GetChild(0).gameObject.SetActive(true);
        }
        foreach (int i in rdIndexList)
        {
            transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = GameManager_SXChuCai.instance.GetSpriteByName(rdChars[cnt1++]);
        }
    }
}
